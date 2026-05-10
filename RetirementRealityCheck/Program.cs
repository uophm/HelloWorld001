using System;

// Based on the "If You Are Over 60 and Still Working in Canada" source math
Console.WriteLine("--- Canadian Retirement 'Invisible Cost' Calculator ---");

// 1. COLLECT USER DATA
Console.Write("Enter Gross Annual Salary: ");
double grossSalary = double.Parse(Console.ReadLine() ?? "0");

Console.Write("Enter Current Age: ");
int age = int.Parse(Console.ReadLine() ?? "0");

Console.Write("Total RRSP Balance: ");
double rrspBalance = double.Parse(Console.ReadLine() ?? "0");

// 2. CALCULATE VISIBLE COSTS (Taxes and Payroll)
// Simplified Ontario/Federal estimates from source [2, 4]
double incomeTax = grossSalary * 0.27; // Estimated average effective rate for $78k
double cppContribution = 4366;        // 2025/26 Max [4]
double eiPremium = 1130;              // 2025/26 Max [5]

double visibleTakeHome = grossSalary - incomeTax - cppContribution - eiPremium;

// 3. CALCULATE INVISIBLE COSTS
// A. Meltdown Cost (Source: [5, 6])
// Every $1 moved from RRSP now (19% tax) avoids future 35% tax on mandatory RRIF.
double annualMeltdownAmount = 45000; 
double meltdownTaxSavingsLost = annualMeltdownAmount * (0.35 - 0.19);

// B. CPP Marginal Return (Source: [7, 8])
// $4,366 contribution only generates ~$1,032 in total lifetime benefits if maxed out.
double cppNetLoss = cppContribution - (51.60 * 20); 

// C. OAS Clawback (Source: [9, 10])
// Threshold ~$91k-$95k. 15% recovery tax on every dollar above.
double oasThreshold = 95323;
double oasClawback = grossSalary > oasThreshold ? (grossSalary - oasThreshold) * 0.15 : 0;

// D. Age Amount Loss (Source: [11, 12])
// Federal credit $1,264 lost if net income > $45,500.
double ageAmountLoss = age >= 65 && grossSalary > 45500 ? 1264 : 0;

// 4. FINAL CALCULATION
double totalInvisibleCosts = meltdownTaxSavingsLost + oasClawback + ageAmountLoss;
double realNetValue = visibleTakeHome - totalInvisibleCosts;

// 5. OUTPUT RESULTS
Console.WriteLine("\n--- Results (One Year of Work) ---");
Console.WriteLine($"Gross Salary:            {grossSalary:C}");
Console.WriteLine($"Visible Take-Home:       {visibleTakeHome:C}");
Console.WriteLine("----------------------------------");
Console.WriteLine($"Lost RRSP Meltdown Value: {meltdownTaxSavingsLost:C}");
Console.WriteLine($"Negative CPP Return:     {cppNetLoss:C}");
Console.WriteLine($"OAS Clawback/Age Credit:  {(oasClawback + ageAmountLoss):C}");
Console.WriteLine("----------------------------------");
Console.WriteLine($"REAL ECONOMIC VALUE:     {realNetValue:C}");
Console.WriteLine($"Efficiency:              {(realNetValue / grossSalary):P1}");

if (realNetValue < (grossSalary * 0.6))
{
    Console.WriteLine("\nADVICE: Working this year is highly inefficient.");
    Console.WriteLine("The 'Gap Year Meltdown' strategy may earn you more in lifetime wealth.");
}