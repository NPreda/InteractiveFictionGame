
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Globalization;
using System.Linq;


public class ExpressionParser 
{

    static readonly string[] operators = { "+", "-", "*", "/", "%", "(", ")", "[", "]" };
    static readonly char[] opChars = operators.SelectMany(x => x.ToCharArray()).Distinct().ToArray();

    //Reward operation variables
    private string[] _operators = { "-", "+", "/", "*", "^" };
    private Func<double, double, double>[] _operations = {
        (a1, a2) => a1 - a2,
        (a1, a2) => a1 + a2,
        (a1, a2) => a1 / a2,
        (a1, a2) => a1 * a2,
        (a1, a2) => Math.Pow(a1, a2)
    };


    public Dictionary<string, int[]> processRewards(string rewards)
    {
        Dictionary<string, int[]> rewardDict = new Dictionary<string, int[]>();
        string[] rewardArray = rewards.Split("\n"[0]);

        foreach (var reward in rewardArray)
        {
            if (reward.Trim() != ""){ 
                var tempDict = (GiveReward(reward));//process rsewards line by line and return dictionaries containing name, oldvalue, newvalue
                try
                {
                    rewardDict = rewardDict.Concat(tempDict).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);
                }
                catch (System.Exception ex)
                {
                    Debug.Log(ex);
                    throw new Exception("Error while merging rewards dictionries for result. Likely duplicate qualities being called involved.");
                }
            }
        }

        return rewardDict;
    }



    public String GetQualityName(String id){
        String qualityName = string.Empty;
        //get the proper name for the quality
        if (RPGStatType.IsDefined(typeof(RPGStatType), id))
            {
                qualityName = id;
            }else if(QualityDB.Instance.inDatabase(id)){
                qualityName = QualityDB.Instance.returnQuality(id).qualityName;
            }
            else
            {
                throw new Exception("Invalid formatting in quality reward field: 'Give to quality' option is deformed or nonexistant ");
            }
        return qualityName;
    }


    public Dictionary<string, int[]> GiveReward(string oneReward)
    {

        //Split the reward into left-side and -right side component
        string qualityID = string.Empty;
        string qualityName = string.Empty;
        int oldValue = 0;
        int newValue = 0;
        string[] splitted = oneReward.Split('=');
        if (splitted.Length > 2)
        {
            throw new Exception("Too many equals in the choice reward operation");
        }

        //Calculate value of that
        newValue =  (int)Mathf.Clamp(Eval(splitted[1]) , 0, Mathf.Infinity); 
        Debug.Log(newValue);
        //Set value of stat using the token in the middle of the brackets
        List<string> tokenized_right = Tokenize(splitted[0]).ToList();
        if (tokenized_right[0] == "[" && tokenized_right[2] == "]")
        {
            qualityID = tokenized_right[1];
            //deal with old and new values of the quality
            oldValue = ReturnValue(qualityID);
            GiveValue(qualityID, newValue);

            //get the proper name for the quality
            qualityName = GetQualityName(qualityID);
        }
        else
        {
            throw new Exception("Invalid formatting in quality reward field: 'Give to quality' option is deformed ");
        }

        //create helpful dictionary for the ui to work with
        var returnDict = new Dictionary<string, int[]>
        {
            { qualityName, new int[]{oldValue,newValue} }
        };
        return returnDict;
    }

    public int ReturnValue(string quality)
    {
        int value = 0;
        Debug.Log(quality);
        Debug.Log(QualityDB.Instance.qualities);
        if (RPGStatType.IsDefined(typeof(RPGStatType), quality))
        {  //case sensitive
            value = Character.Instance.statManager.GetStatBaseValue(quality);
        }else if(QualityDB.Instance.inDatabase(quality)){
            value = Inventory.Instance.ReturnItemValue(quality);
        }
        else
        {
            throw new Exception("Invalid formatting in quality reward field: 'Give to quality' option is deformed or nonexistant ");
        }
        Debug.Log(string.Format("Returning value {0} for quality {1}", value, quality));
        return value;
    }

    //give the named quality this value.
    public void GiveValue(string qualityID, int value)
    {     
        Debug.Log(string.Format("Giving quality {0} the value {1}", qualityID, value));
        if (RPGStatType.IsDefined(typeof(RPGStatType), qualityID))    //check if the property being given the value is a stat
        {  //case sensitive
            Character.Instance.statManager.statSet(qualityID, value);
        }else if(QualityDB.Instance.inDatabase(qualityID)){           //check if the property being given the value is a quality
            var quality = QualityDB.Instance.returnQuality(qualityID);
            Inventory.Instance.AddItem(quality, value);
        }
        else
        {
            throw new Exception("Invalid formatting in quality reward field: 'Give to quality' option is deformed or nonexistant");
        }
    }


    public int Eval(string expression)               //Evaluate the left side operation to give a value
    {
        List<string> tokens = Tokenize(expression).ToList();
        Stack<double> operandStack = new Stack<double>();
        Stack<string> operatorStack = new Stack<string>();
        int tokenIndex = 0;

        while (tokenIndex < tokens.Count)
        {
            string token = tokens[tokenIndex];
            //if it's square braket, get the value of the quality
            if (token == "[")
            {
                if (tokens[tokenIndex + 2] == "]")
                {       //check if the expression is properly formated
                    operandStack.Push(ReturnValue(tokens[tokenIndex + 1]));
                    tokenIndex = tokenIndex + 3;
                    continue;
                }
                else
                {
                    throw new ArgumentException("Invalid formatting in quality reward field:  a quality reference is deformed ");
                }
            }

            if (token == "]")
            {
                throw new ArgumentException("Mis-matched  quality marking square parentheses in expression");
            }

            //if it's a round bracket, get sub-expression
            if (token == "(")
            {
                string subExpr = getSubExpression(tokens, ref tokenIndex);
                operandStack.Push(Eval(subExpr));
                continue;
            }
            if (token == ")")
            {
                throw new ArgumentException("Mis-matched parentheses in expression");
            }

            //If this is an operator  
            if (Array.IndexOf(_operators, token) >= 0)
            {
                while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                {
                    string op = operatorStack.Pop();
                    double arg2 = operandStack.Pop();
                    double arg1 = operandStack.Pop();
                    operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                }
                operatorStack.Push(token);
            }
            else
            {
                Debug.Log(token);
                operandStack.Push(double.Parse(token));
            }
            tokenIndex += 1;
        }

        while (operatorStack.Count > 0)
        {
            string op = operatorStack.Pop();
            double arg2 = operandStack.Pop();
            double arg1 = operandStack.Pop();
            Debug.Log("Operations");
            operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
        }
        return (int)Mathf.Floor((float)operandStack.Pop());
    }

    private string getSubExpression(List<string> tokens, ref int index)
    {
        StringBuilder subExpr = new StringBuilder();
        int parenlevels = 1;
        index += 1;
        while (index < tokens.Count && parenlevels > 0)
        {
            string token = tokens[index];
            if (tokens[index] == "(")
            {
                parenlevels += 1;
            }

            if (tokens[index] == ")")
            {
                parenlevels -= 1;
            }

            if (parenlevels > 0)
            {
                subExpr.Append(token);
            }

            index += 1;
        }

        if ((parenlevels > 0))
        {
            throw new ArgumentException("Mis-matched parentheses in expression");
        }
        return subExpr.ToString();
    }

    public static IEnumerable<string> Tokenize(string input)
    {
        var buffer = new StringBuilder();
        foreach (char c in input)
        {
            if (char.IsWhiteSpace(c))
            {
                if (buffer.Length > 0)
                {
                    yield return Flush(buffer);
                }
                continue; // just skip whitespace
            }

            if (IsOperatorChar(c))
            {
                if (buffer.Length > 0)
                {
                    // we have back-buffer; could be a>b, but could be >=
                    // need to check if there is a combined operator candidate
                    if (!CanCombine(buffer, c))
                    {
                        yield return Flush(buffer);
                    }
                }
                buffer.Append(c);
                continue;
            }

            // so here, the new character is *not* an operator; if we have
            // a back-buffer that *is* operators, yield that
            if (buffer.Length > 0 && IsOperatorChar(buffer[0]))
            {
                yield return Flush(buffer);
            }

            // append
            buffer.Append(c);
        }
        // out of chars... anything left?
        if (buffer.Length != 0)
            yield return Flush(buffer);
    }
    static string Flush(StringBuilder buffer)
    {
        string s = buffer.ToString();
        buffer.Clear();
        return s;
    }

    static bool IsOperatorChar(char newChar)
    {
        return Array.IndexOf(opChars, newChar) >= 0;
    }

    static bool CanCombine(StringBuilder buffer, char c)
    {
        foreach (var op in operators)
        {
            if (op.Length <= buffer.Length) continue;
            // check starts with same plus this one
            bool startsWith = true;
            for (int i = 0; i < buffer.Length; i++)
            {
                if (op[i] != buffer[i])
                {
                    startsWith = false;
                    break;
                }
            }
            if (startsWith && op[buffer.Length] == c) return true;
        }
        return false;
    }

    //THIS ONLY APPLIES TO CHOICE CONDITIONS
    //checker methods to be used for character stats
    public Tuple<Dictionary<string, bool>, bool, bool> checkConditions(StatCondition[] statConditions)    //return Tuple({Needs: Success, general success, ifHidden})
    {
        bool failed = false;
        bool hidden = false;
        Dictionary<string, bool> returnDict = new Dictionary<string, bool>();

        foreach (var condition in statConditions)
        {
            var result = EvalCompare(condition);
            var text = TextifyCondition(condition);
            //print(string.Format("{0} with result {1}", text, result));
            if (result == false){
                failed = true;
            }
            if (condition.hideOnFalse == true && result == false){
                hidden = true;
            }
            returnDict[text] = result;
        }

        return new Tuple<Dictionary<string, bool>, bool, bool>(returnDict,failed, hidden);
    }

    private bool EvalCompare(StatCondition condition)
    {
        int statValue = ReturnValue(condition.quality);
        int op = (int)condition.operation;   //get int value for the enumerator of the comparison type
        int checkValue = condition.value;     //the value which to check against
        switch (op)
        {
            case 0: return (statValue == checkValue);
            case 1: return (statValue > checkValue);
            case 2: return (statValue >= checkValue);
            case 3: return (statValue < checkValue);
            case 4: return (statValue <= checkValue);
            case 5: return (statValue != checkValue);
            default: 
                throw new Exception("You did something really terrible with your choice condition");
        }
    }

    private string TextifyCondition(StatCondition condition)
    {
        int op = (int)condition.operation;   //get int value for the enumerator of the comparison type
        
        //get the proper name for the quality
        var qualityName = GetQualityName(condition.quality);
        switch (op)
        {
            case 0: return (string.Format("{0} equal to {1}", qualityName, condition.value));
            case 1: return (string.Format("{0} greater than {1}", qualityName, condition.value));
            case 2: return (string.Format("{0} greater or equal than {1}", qualityName, condition.value));
            case 3: return (string.Format("{0} less than {1}", qualityName, condition.value));
            case 4: return (string.Format("{0} less or equal than {1}", qualityName, condition.value));
            case 5: return (string.Format("{0} not equal to {1}", qualityName, condition.value));
            default: 
                throw new Exception("You did something really terrible with your choice condition");
        }
    }
}
