using System;

interface ILivingBeing
{
    void WayOfExpression();
}

class HumanBeing : ILivingBeing{

    private string gender;
    private int age;

    public string GetGender(){
        return gender;
    }

    public void SetGender(string gender){
        this.gender = gender;
    }

    public int GetAge(){
            return age;
    }

    public void SetAge(int age){
            if (age >= 0) this.age = age;
            else Console.WriteLine("Please pass a nonnegative value");
    }

    public void WayOfExpression(){
        Console.WriteLine("humans talk");
    }
}

class Animal : ILivingBeing{
    public virtual void WayOfExpression(){
        Console.WriteLine("animals make animal sounds");
    }
}

abstract class Plant : ILivingBeing{
    public void WayOfExpression(){
        Console.WriteLine("plants can not produce sounds");
    }
}

class Cat : Animal{
    public override void WayOfExpression(){
        Console.WriteLine("The cat says: mijau");
    }
}

class Dog : Animal{
    public override void WayOfExpression(){
        Console.WriteLine("The dog says: bark bark");
    }
}

class Cow : Animal{
    public override void WayOfExpression(){
        Console.WriteLine("The cow says: moo");
    }

}

class Rose : Plant{ 
    public void Height<T>(T height){
        Console.WriteLine(height + " cm");  //nista pametnije mi nije palo na pamet za nesto genericko u ovom kontekstu
    }   

}



class Program
{
  static void Main(string[] args)
  {
    Animal pet = new Animal();
    Console.Write("Pet is an animal and ");
    pet.WayOfExpression();

    Cat persianCat = new Cat();
    persianCat.WayOfExpression();
 
    Dog germanShepherd = new Dog();
    germanShepherd.WayOfExpression();

    Cow whiteCow = new Cow();
    whiteCow.WayOfExpression();

    HumanBeing Biannca = new HumanBeing();
    Biannca.SetGender("female");
    //Biannca.SetAge(-1); -> output: "Please pass a nonnegative value"
    Biannca.SetAge(20);
    Console.WriteLine("Biannca is a " + Biannca.GetGender() + " who is " + Biannca.GetAge() + " years old");

    //Plant rose = new Plant(); -> "Cannot create an instance of the abstract class or interface 'Plant'"
    Rose redRose = new Rose();
    Console.Write("Red rose is a plant and ");
    redRose.WayOfExpression();
    Console.Write("Height of red rose is ");
    redRose.Height<int>(22);

    Rose whiteRose = new Rose();
    Console.Write("Height of white rose is ");
    whiteRose.Height<string>("twenty-five");
  }
  
}

