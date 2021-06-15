using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MitchellP1
{
    class Program
    {
        static void Main(string[] args)
        
       {
            DisplayHeader();

            string another = " ";
            do
            {
                

                string[] gasNames = new string[100];
                double[] molecularWeights = new double[100];
                int count = 0;

                string gasName = " ";
                int countGases = 0;
                double moleWeight = 0.0;
                double mass = 0.0;
                double molecularWeight = 0.0;
                double vol = 0.0;
                double temp = 0.0;
                double pascals = 0.0;



                GetMolecularWeights(ref gasNames, ref molecularWeights, out count);

                DisplayGasNames(gasNames, countGases);

                Console.WriteLine("Enter the gas name you would like to use.");
                gasName = Console.ReadLine();

                string searchString = gasName;
                countGases = Array.IndexOf(gasNames, gasName);


                if (gasNames.Contains(gasName))
                {
                   

                    molecularWeight = GetMolecularWeightFromName(gasName, gasNames, molecularWeights, countGases);

                    pascals = Pressure(mass, vol, temp, molecularWeight);

                    DisplayPressure(pascals);
                

                    Console.WriteLine("Would you like to find the pressure of another gas? Please enter yes or no.");
                    another = Console.ReadLine();

                }
                else
                {
                    Console.WriteLine("The name you entered is not on the list.");
                     
                    return;
                }

            } while (another == "yes");

            Console.WriteLine("Have a nice day. Goodbye.");
           


        }



        static void DisplayHeader()
        {
            Console.WriteLine("The Ideal Gas Equation Calculator");
        }



        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {
            string[] readText = File.ReadAllLines("MolecularWeightsGasesAndVapors (1).csv");
            readText = readText.Where(i => !string.IsNullOrEmpty(i)).ToArray(); //Trying to remove the extra null elements in array
            for (int i = 1; i < readText.Length; ++i)
            {
                //Console.WriteLine(readText[i]);
                string[] text = readText[i].Split(",");
                
                gasNames[i] = text[0];
                molecularWeights[i] = Convert.ToDouble(text[1]);
                //Console.WriteLine(molecularWeights[i]);  //Checking mole weights print                
                
            }
            count = readText.Length;
        }



        private static void DisplayGasNames(string[] gasNames, int countGases)
        {

            //display the names of gases to the console in 3 cols
            Console.WriteLine("The gas names are: ");
            Console.WriteLine($"{ " First Column",-25 }\t{ "Second Column",-25}\t{ "Third Column",-25}");
            Console.WriteLine($"{" _____________",-25}\t{"______________",-25}\t{"______________",-25}");

            for (int i = 1; i < gasNames.Length - 2; i += 3)
            {
                
                Console.WriteLine(gasNames[i] + "\t\t\t" + gasNames[i + 1] + "\t\t\t" + gasNames[i + 2]);
            }

        }



        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {

            double moleWeight = 0.0;
            //fct looks up the name of a gas in an array and returns the mole weight the the gass in mols
            Console.WriteLine("The gas name " + $"{ gasName }, is at index {countGases}. ");
            moleWeight = molecularWeights[countGases];
            Console.WriteLine("The molecular weight of your gas is " + $"{moleWeight}.");



            return moleWeight; 
        }



        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {

            double pascals = 0.0;
            double numberMoles = 0.0;
            const double r = 8.3145;            
            double kelvin = 0.0;

            //Console.WriteLine($"Word{molecularWeight}");

            Console.WriteLine("Please enter the volume of your container in cubic meters: ");
            vol = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Please enter the mass of the gas in grams: ");
            mass = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Please enter the temperature of the gas in Celcius: ");
            temp = Convert.ToDouble(Console.ReadLine());

            numberMoles = NumberOfMoles(mass, molecularWeight);

            kelvin = CelsiusToKelvin(temp);

            pascals = (numberMoles * r * kelvin) / vol;
            

            return pascals;
        }




        static double NumberOfMoles(double mass, double molecularWeight)
        {
            
            double numberMoles = mass / molecularWeight;

            Console.WriteLine("The number of moles is " + $"{numberMoles}"); 

            return numberMoles;
        }



        static double CelsiusToKelvin(double temp)
        {

            temp = temp + 273.15;
            //Console.WriteLine("Kelvin temp is " + $"{temp}");
            return temp;
        }


        private static void DisplayPressure(double pascals)
        {
            double psi = 0.0;
            
            Console.WriteLine("The pressure of your gas in Pascals is " + $"{pascals}");
            psi = PaToPSI(pascals);
            Console.WriteLine("The pressure of your gas in psi is " + $"{psi}.");

        }



        static double PaToPSI(double pascals)
        {
            
            double psi = 0.0;

            psi = pascals / 6895;
           
            return psi;
        }

    } 
}
