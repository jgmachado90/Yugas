using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attribute
{
    Dark,
    Earth,
    Fire,
    Light,
    Water,
    Wind,
    SpellCard,
    TrapCard,
    Ritual
}

public enum GuardianStar
{
    Sun,         // Sol
    Moon,        // Lua
    Mercury,     // Mercúrio
    Venus,       // Vênus
    Mars,        // Marte
    Jupiter,     // Júpiter
    Saturn,      // Saturno
    Uranus,      // Urano
    Pluto,       // Plutão
    Neptune,     // Netuno
}

public enum CardType
{
    Monster,
    SpellCard,
    TrapCard,
    Ritual
}

public enum MonsterType
{
    None,          
    Dragon,
    Spellcaster,
    Zombie,
    Warrior,
    BeastWarrior,
    Beast,
    WingedBeast,
    Fiend,
    Fairy,
    Insect,
    Dinosaur,
    Reptile,
    Fish,
    SeaSerpent,
    Thunder,
    Aqua,
    Pyro,
    Rock,
    Plant,
    Machine
}


[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    public string cardName;
    public Attribute attribute;
    public string description;
    public int level;
    public GuardianStar primaryStar;
    public GuardianStar secondaryStar;
    public CardType cardType;
    public MonsterType monsterType;
    public int atk;
    public int def;
    public Sprite cardImage;
    public List<string> specifications;
}
