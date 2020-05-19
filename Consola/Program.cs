﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using JVNM;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.IO;
using System.Threading;

namespace Consola
{
    public class Program
    {


        public static void Main(string[] args)
        {
            
            int puerto = 1200;
            string server = "127.0.0.1";
            TcpClient client = new TcpClient(server, puerto);


            Console.WriteLine("Write the name of the database: ");
            string nameDatabase = Console.ReadLine();
            Console.WriteLine("Write the name of the user: ");
            string nameUser = Console.ReadLine();
            Console.WriteLine("Write the password of the user: ");
            string pass = Console.ReadLine();

            //Database1,admin,admin
            string login = nameDatabase + "," + nameUser + "," + pass;

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(login);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            //Console.WriteLine("Sent: {0}", login);

            Byte[] re = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(re, 0, re.Length);
            responseData = System.Text.Encoding.ASCII.GetString(re, 0, bytes);

            Boolean log = false;


            //AÑADIR ERRORESSS
            if (responseData.Equals(Query.SecurityUserDoesNotExist) || responseData.Equals(Query.SecurityIncorrectLogin))
            {
                Console.WriteLine("Received: {0}", responseData);
                Thread.Sleep(3000);
                client.Close();

            }
            else if (responseData.Equals(Query.OpenDatabaseSuccess) || responseData.Equals(Query.CreateDatabaseSuccess))
            {
                log = true;
                Console.WriteLine(responseData);

            }
            if(log ==true)
            {                 
                Console.WriteLine("Write the sentences: ");
                login = Console.ReadLine();
                
                while (!login.Contains("stop"))
                {

                    data = System.Text.Encoding.ASCII.GetBytes(login);
                    stream.Write(data, 0, data.Length);
                    //Console.WriteLine("Sent: {0}", login);
                                         
                    responseData = String.Empty;

                    bytes = stream.Read(re, 0, re.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(re, 0, bytes);

                    Console.WriteLine(responseData);

                    Console.WriteLine("Write the sentences: ");
                    login = Console.ReadLine();
                }               
            }
        }
    }
}
