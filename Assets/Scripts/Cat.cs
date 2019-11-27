using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cat 
{
    public string name;
    private int age; // поле ещё нужно?* 
    public int Age
    {
        get { return age; }
    }

    private int Weight { get; set; }
    public int height;
    public int tailLength;
    
    public Cat()
    {
        name = "Murka";
        age = 1;
        tailLength = 25;
    }

    public Cat(string name, int age, int Weight, int height, int tailLength)
    {
        this.name = name;
        this.age = age;
        this.Weight = Weight;
        this.height = height;
        this.tailLength = tailLength;
    }


    public void Meow()
    {
        Debug.Log("Кошка по имени " + name + ", возрастом " + Age + " лет, " + "вес " + Weight + ", ростом " + height + " и длиной хвоста " + tailLength);
    }
   
}
