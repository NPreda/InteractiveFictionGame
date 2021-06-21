using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class CombatLogParser
{

    /*
    <v> - value expressed normally
    <d> - value expressed in red
    <h> - value expressed in green
    <l> - value expressed in purple
    <m> - magic expressed in blue
    */


    private string sourceTag = "<source>";
    private string targetTag = "<target>";

    private string valueTag = "<v>";
    private string damageTag = "<d>";
    private string healTag = "<h>";
    private string lustTag = "<l>";
    private string magicTag = "<m>";

    //these get golden, capsed and in bold
    private List<string> keyWords =  new List<string>() {"prepared", "strikes", "strike", "damage",  "block",  "speed", "status",  "resolve", "heal", "bleed", "wound", "stun"};


    private string sourceString;
    private string targetString;
    private string valueString;


    public string ParseEntry(string template, ITarget source, ITarget target, int effectValue)
    {
        this.sourceString = FormatName(source);
        this.targetString = FormatName(target);
        this.valueString = "<b>" + effectValue.ToString()+"</b>";

        return ProcessTemplate(template);
    }

    public string ParseEntry(string template, int effectValue)
    {
        this.valueString = "<b>" + effectValue.ToString()+"</b>";

        return ProcessTemplate(template);
    }

    private string ProcessTemplate(string template)
    {
        template = template.Replace(sourceTag, sourceString);
        template = template.Replace(targetTag, targetString);

        //damage number logic
        template = template.Replace(valueTag, valueString);
        template = template.Replace(damageTag,  "<color=\"red\">"+ valueString +"</color>");
        template = template.Replace(healTag,  "<color=\"green\">"+ valueString +"</color>");
        template = template.Replace(lustTag,  "<color=\"purple\">"+ valueString +"</color>");
        template = template.Replace(magicTag,  "<color=\"blue\">"+ valueString +"</color>");

        //custom words logic
        foreach(string word in keyWords)
        {
            template = Regex.Replace(template, word, "<b>"+ CaseString(word) +"</b>", RegexOptions.IgnoreCase);
        }

        return template;
    }

    private string FormatName(ITarget unit)
    {
        string unitString = "";
        switch(unit.allegiance)
        {
            case TargetType.Player:
                unitString = "<color=\"blue\"><b>"+ unit.unitName +"</b></color>";
                break;
            case TargetType.Friend:
                unitString = "<color=\"green\"><b>"+ unit.unitName +"</b></color>";
                break;
            case TargetType.Foe:
                unitString = "<color=\"red\"><b>"+ unit.unitName +"</b></color>";
                break;
            default:
                unitString = "<b>"+ unit.unitName +"</b>";
                break;
        }
        return unitString;
    }

    /// Returns the input string with the first character converted to uppercase, or mutates any nulls passed into string.Empty
    private string CaseString(string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;

        s.ToLower();    //make sure there is no weirdness midstring

        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}
