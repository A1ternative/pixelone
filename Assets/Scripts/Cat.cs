using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cat 
{
    public string name;
    public int age;
    public int weight;
    public int height;
    public int tailLenght;
    
    public Cat()
    {
        name = "Murka";
        age = 1;
        tailLenght = 25;
    }

    public Cat(string name, int age, int weight, int height, int tailLenght)
    {
        this.name = name;
        this.age = age;
        this.weight = weight;
        this.height = height;
        this.tailLenght = tailLenght;
    }


    public void Meow()
    {
        Debug.Log("Кошка по имени " + name + ", возрастом " + age + " лет, " + "вес " + weight + ", ростом " + height + " и длиной хвоста " + tailLenght);
    }
   
}
