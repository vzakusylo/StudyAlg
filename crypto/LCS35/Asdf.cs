//using System;
//using System.IO;
//using System.Numerics;
//using System.Text;

//public class TimeLockPuzzle {
    
//    public TimeLockPuzzle() {
        
//    }
    
    
//    public static void main(String[] args) {
//        Console.WriteLine("Creating LCS35 Time Capsule Crypto-Puzzle...");
//        TimeLockPuzzle.CreatePuzzle();
//        Console.WriteLine("Puzzle Created.");
//    }
    
//    public static void CreatePuzzle() {
//        //  Compute count of squarings to do each year
//        BigInteger squaringsPerSecond = new BigInteger("3000");
//        //  in 1999
//        Console.WriteLine(("Assumed number of squarings/second (now) = " + squaringsPerSecond));
//        BigInteger secondsPerYear = new BigInteger(31536000);
//        BigInteger squaringsFirstYear = secondsPerYear * squaringsPerSecond;
//        Console.WriteLine(("Squarings (first year) = " + squaringsFirstYear));
//        int years = 35;
//        BigInteger t = new BigInteger(0);
//        BigInteger s = squaringsFirstYear;
//        for (int i = 1999; (i <= (1998 + years)); i++) {
//            //  do s squarings in year i
//            t = t.Add(s);
//            //  apply Moore's Law to get number of squarings to do the next year
//            String growth = new String("");
//            growth = "12204";
//            //  ~x13 up to 2012, at constant rate
//            if ((i > 2012)) {
//                growth = "10750";
//            }
            
//            //  ~x5  up to 2034, at constant rate
//            s = s * (new BigInteger(growth)) /(new BigInteger(10000));
//        }
        
//        Console.WriteLine(("Squarings (total)= " + t));
//        Console.WriteLine(("Ratio of total to first year = " + t /(squaringsFirstYear)));
//        Console.WriteLine(("Ratio of last year to first year = " +
//                           s / (squaringsFirstYear * (new BigInteger(10758)) / (new BigInteger(10000)))));
//        //  Now generate RSA parameters    
//        int primelength = 1024;
//        Console.WriteLine(("Using " 
//                        + (primelength + "-bit primes.")));
//        BigInteger twoPower = new BigInteger(1).shiftLeft(primelength);
//        String pseed = TimeLockPuzzle.getString("large random integer for prime p seed");
//        BigInteger prand = new BigInteger(pseed);
//        String qseed = TimeLockPuzzle.getString("large random integer for prime q seed");
//        BigInteger qrand = new BigInteger(qseed);
//        Console.WriteLine("Computing...");
//        BigInteger p = new BigInteger(5);
//        //  Note that 5 has maximal order modulo 2^k (See Knuth)
//        p = TimeLockPuzzle.getNextPrime(p.modPow(prand, twoPower));
//        Console.WriteLine(("p = " + p));
//        BigInteger q = new BigInteger(5);
//        q = TimeLockPuzzle.getNextPrime(q.modPow(qrand, twoPower));
//        Console.WriteLine(("q = " + q));
//        BigInteger n = p*(q);
//        Console.WriteLine(("n = " + n));
//        BigInteger pm1 = p -(ONE);
//        BigInteger qm1 = q- (ONE);
//        BigInteger phi = pm1 * (qm1);
//        Console.WriteLine(("phi = " + phi));
//        //  Now generate final puzzle value w
//        BigInteger u = TWO.modPow(t, phi);
//        BigInteger w = TWO.modPow(u, n);
//        Console.WriteLine(("w (hex) = " + w.toString(16)));
//        //  Obtain and encrypt the secret message
//        //  Include seed for p as a check
//        StringBuffer sgen = new StringBuffer(TimeLockPuzzle.getString("string for secret"));
//        sgen = sgen.append((" (seed value b for p = " 
//                        + (prand.toString() + ")")));
//        Console.WriteLine(("Puzzle secret = " + sgen));
//        BigInteger secret = TimeLockPuzzle.getBigIntegerFromStringBuffer(sgen);
//        if ((secret.compareTo(n) > 0)) {
//            Console.WriteLine("Secret too large!");
//            return;
//        }
        
//        BigInteger z = secret.xor(w);
//        Console.WriteLine(("z(hex) = " + z.toString(16)));
//        //  Write output to a file
//        PrintWriter pw = new PrintWriter(new FileWriter("C:\\\\puzzleoutput.txt"));
//        pw.println("Crypto-Puzzle for LCS35 Time Capsule.");
//        pw.println("Created by Ronald L. Rivest. April 2, 1999.");
//        pw.println();
//        pw.println("Puzzle parameters (all in decimal):");
//        pw.println();
//        pw.print("n = ");
//        TimeLockPuzzle.printBigInteger(n, pw);
//        pw.println();
//        pw.print("t = ");
//        TimeLockPuzzle.printBigInteger(t, pw);
//        pw.println();
//        pw.print("z = ");
//        TimeLockPuzzle.printBigInteger(z, pw);
//        pw.println();
//        pw.println("To solve the puzzle, first compute w = 2^(2^t) (mod n).");
//        pw.println("Then exclusive-or the result with z.");
//        pw.println("(Right-justify the two strings first.)");
//        pw.println();
//        pw.println("The result is the secret message (8 bits per character),");
//        pw.println("including information that will allow you to factor n.");
//        pw.println("(The extra information is a seed value b, such that ");
//        pw.println("5^b (mod 2^1024) is just below a prime factor of n.)");
//        pw.println(" ");
//        pw.close();
//        //  Wait for input carriage return to pause before closing
//        Console.Read();
//    }
    
//    static BigInteger ONE = new BigInteger(1);
//    static BigInteger TWO = new BigInteger(2);
    
//    static String getString(String what) {
//        //  This routine is essentially a prompted "readLine"
//        StringBuilder s = new StringBuilder();
//        Console.WriteLine(("Enter " 
//                        + (what + " followed by a carriage return:")));
//        for (int i = 0; (i < 1000); i++) {
//            int c = Console.Read();
//            if (((c < 0) 
//                        || (c == '\n'))) {
//                break;
//            }
            
//            if ((c != '\r')) {
//                s = s.Append(((char)(c)));
//            }
            
//        }
        
//        return s.ToString();
//    }
    
//    static BigInteger getBigIntegerFromStringBuffer(StringBuilder s) {
//        //  Base-256 interpretation of the given string
//        BigInteger randbi = new BigInteger("0");
//        for (int i = 0; (i < s.Length()); i++) {
//            int c = s.charAt(i);
//            randbi = randbi.shiftLeft(8).add(new BigInteger(Integer.toString(c)));
//        }
        
//        Console.WriteLine(("Value of string entered (hex) = " + randbi.toString(16)));
//        return randbi;
//    }
    
//    static void printBigInteger(BigInteger x, PrintWriter pw) {
//        String s = x.toString();
//        int charsPerLine = 60;
//        for (int i = 0; (i < s.length()); i = (i + charsPerLine)) {
//            if ((i != 0)) {
//                pw.println();
//                pw.print("    ");
//            }
            
//            pw.print(s.substring(i, java.lang.Math.min((i + charsPerLine), s.length())));
//        }
        
//        pw.println();
//    }
    
//    static BigInteger getNextPrime(BigInteger startvalue) {
//        BigInteger p = startvalue;
//        if (!p + (ONE).equals(ONE)) {
//            p = p.add(ONE);
//        }
        
//        while (!p.isProbablePrime(40)) {
//            p = p.add(TWO);
//        }
        
//        return;
//    }
//}

//internal class PrintWriter
//{
//}
////  end of TimeLockPuzzle class