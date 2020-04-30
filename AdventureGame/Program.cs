using System;
using System.IO;

namespace Adventure
{
    class MainClass
    {
        //constante de sobredimension del mapa
        const int N = 100;
        //string con el nombre del archivo (lo hago estatico para que no sea necesario introducirlo como parametro en SaveGame
        static string file;

        static void Main()
        {
            //booleano que controla si el jugador ha decidido salir
            bool quit;
            Map map = new Map(N, N); //creamos un mapa sobredimensionado
            Player player; //variable del jugador
            int opciones = Menu(); //peticion de opcion al usuario

            //si elige nueva partida
            if (opciones == 2) CreateMap(map, out player, out file); //establecemos los datos del archivo en el mapa
            else RestoreGame(map, out player); //si elige continuar, restauramos el estado del juego

            //preguntamos si quiere insertar un archivo
            CommandFile(player, map, out quit);

            //bucle principal de juego
            while (!ArrivedAtExit(map, player) && player.IsAlive() && !quit)
            {
                string command; //comando a introducir por el jugador
                do
                {
                    Console.Write(">"); //escribimos el prompt
                    command = Console.ReadLine(); //leemos el comando del jugador
                } while (!HandleInput(command, player, map, out quit)); //hacemos el bucle hasta que el comando sea válido
            }

            //imprimimos en pantalla un mensaje acorde a por qué se ha acabado el juego
            FinalMessage(player, quit);
        }

        static bool HandleInput(string com, Player p, Map m, out bool quit) //metodo que procesa el input del usuario
        {
            bool validCommand = true; //booleano de validez del comando
            quit = false; //establecemos el booleano de salida a false temporalmente
            string[] command = com.Trim().Split(' '); //dividimos el comando
            switch (command[0]) //comprobamos el inicio del comando
            {
                case "go": //caso go (movimiento)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 2) throw new Exception("Invalid command.");
                        int dir = GetDirection(command[1]); //guardamos la direccion
                        if (dir != -1) //si es valida
                        {
                            //comprobamaos si el movimiento es válido
                            //si no es valido, lanzamos excepcion
                            if (!p.Move(m, (Direction)dir)) throw new Exception("There's no connection in that direction.");
                            else InfoPlace(m, p.GetPosition()); //si es valido, imprimimos la informacion de la nueva sala
                        }
                        else throw new Exception("Invalid direction.");
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "pick": //caso pick (recoger item)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 2) throw new Exception("Invalid command.");
                        p.PickItem(m, command[1]); //en caso contrario, cogemos el item
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "look": //caso look (onjetos en la sala)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 1) throw new Exception("Invalid command.");
                        //en caso contrario, imprimimos la informacion de los items de la sala actual
                        Console.WriteLine(m.GetInfoItemsInRoom(p.GetPosition()));
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "info": //caso info (movimientos posibles)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 1) throw new Exception("Invalid command.");
                        //en caso contrario, imprimimos la informacion de la sala actual
                        InfoPlace(m, p.GetPosition());
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "inventory": //caso inventory (informacion inventario)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 1) throw new Exception("Invalid command.");
                        //en caso contrario, imprimimos la informacion de los items del inventario
                        Console.WriteLine(p.GetInventoryInfo(m));
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "eat": //caso eat (comer item)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 2) throw new Exception("Invalid command.");
                        p.EatItem(m, command[1]); //en caso contrario, ingerimos el item
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "drop": //caso drop (soltar item en sala)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 2) throw new Exception("Invalid command.");
                        p.DropItem(m, command[1]); //en caso contrario, soltamos el item
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "me": //caso me (informacion del jugador)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 1) throw new Exception("Invalid command.");
                        //en caso contrario, imprimimos la informacion del jugador
                        Console.WriteLine(p.GetPlayerInfo());
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "quit": //caso quit (salir del juego)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 1) throw new Exception("Invalid command.");
                        //en caso contrario, guardamos partida y salimos (quit == true)
                        SaveGame(p, m);
                        quit = true;
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "help": //caso help (movimiento)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 1) throw new Exception("Invalid command.");
                        //en caso contrario, imprimimos la informacion de los comandos
                        Console.WriteLine(Commands());
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                case "clear": //caso clear (para depurar, comando de limpieza de pantalla)
                    try
                    {
                        //si el comando no cumple su formato, lanzamos excepcion
                        if (command.Length != 1) throw new Exception("Invalid command.");
                        //en caso contrario, limpiamos panntalla
                        Console.Clear();
                    }
                    catch (Exception e) //en caso de haber ocurrido una excepcion
                    {
                        Console.WriteLine(e.Message); //escribimos su mensaje
                        validCommand = false; //establecemos la validez del comando a falso
                    }
                    break;
                default: //en caso de no ser ninguno de esos comandos 
                    Console.WriteLine("Invalid command.");
                    validCommand = false;
                    break;
            }

            //devolvemos si el comando es válido o no
            return validCommand;
        }

        static int Menu() //metodo que hace de menu para el jugador
        {
            int opcion = 2; //iniciamos la opcion a 2 (Nueva partida)
            if (File.Exists("partidaGuardada.txt")) //de existir la partida guardada
            {
                do //pedimos input al usuario hasta que haya seleccionado una opcion valida (Continuar o Nueva partida)
                {
                    try
                    {
                        Console.WriteLine("Select an option:\n1. Continue game." +
                        "\n2. New game (this will overwrite the save file when you quit).");
                        opcion = int.Parse(Console.ReadLine());
                    }
                    catch //si no escribe bien la opcion
                    {
                        Console.Clear(); //obligamos a que se repita el bucle
                        Console.WriteLine("Write the answer in the correct format (1 or 2).");
                        opcion = -1;
                    }
                }
                while (opcion < 1 || opcion > 2);
            }

            //devolvemos la opcion seleccionada (o 2 automaticamente en caso de no haber partida guardada)
            return opcion;
        }

        static void CreateMap(Map map, out Player player, out string file) //metodo de creacion del mapa
        {
            bool mapaCorrecto = false; //booleano que controla si el mapa creado es correcto
            file = ""; //asignacion temporal del archivo de lectura

            do
            {
                Console.Write("Insert map file (with extension): ");
                try
                {
                    file = Console.ReadLine(); //asignacion del archivo de lectura por parte del usuario
                    //inicializamos el mapa en base a un archivo
                    map.ReadMap(file);
                    mapaCorrecto = true; //en caso de no dar excepcion, es que el mapa es correcto
                }
                catch (Exception e) //en caso de que haya alguna excepcion
                {
                    Console.Clear(); //limpiamos pantalla
                    //escribimos su mensaje
                    Console.WriteLine(e.Message);
                }
            } while (!mapaCorrecto); //hacemos el bucle hasta que el mapa se haya creado correctamente

            //confirmamos que el mapa se ha creado con exito, y solicitamos el nombre del jugador
            Console.Write("Map correctly made.\nPlayer name: ");
            //creamos un jugador en base a dicho nombre
            player = new Player(Console.ReadLine(), map.GetEntryRoom());
            //limpiamos la pantalla
            Console.Clear();
        }

        static void SaveGame(Player player, Map map) //metodo para guardar la partida
        {
            //borramos la anterior instancia de la partida guardada en caso de que existiera
            if (File.Exists("partidaGuardada.txt")) File.Delete("partidaGuardada.txt");

            //creamos un flujo de lectura
            StreamWriter guardado = new StreamWriter("partidaGuardada.txt");
            guardado.WriteLine(file + "\n"); //primera linea:archivo del que se creo el mapa + salto de linea
            //linea: informacion de los datos guardados del jugador
            guardado.WriteLine(@"PLAYER //name HP weight pos \n itemsInInventory");
            player.SavePlayer(guardado); //guardamos dichos datos del jugador
            //linea: informacion de los datos guardados del mapa
            guardado.WriteLine("\nMAP //roomNumber itemNumber");
            map.SaveMap(guardado); //guardamos los datos del mapa (localizacion de items)

            guardado.Close(); //cerramos flujo de lectura
        }

        static void RestoreGame(Map map, out Player player) //metodo para restaurar la partida
        {
            //creamos flujo de lectura
            StreamReader partidaGuardada = new StreamReader("partidaGuardada.txt");

            map.ReadMap(partidaGuardada.ReadLine()); //creamos el mapa con el archivo original, sin ninguna variacion

            //saltamos dos lineas
            partidaGuardada.ReadLine();
            partidaGuardada.ReadLine();

            //leemos la linea, y la dividimos
            string[] playerInfo = partidaGuardada.ReadLine().Split(' ');
            //creamos al jugador con los valores de la linea
            player = new Player(playerInfo[0], int.Parse(playerInfo[1]), int.Parse(playerInfo[2]), int.Parse(playerInfo[3]));
            //restauramos el inventario con respecto a la siguiente linea
            player.RestoreInventory(partidaGuardada.ReadLine().Split(' '), map);

            //saltamos dos lineas
            partidaGuardada.ReadLine();
            partidaGuardada.ReadLine();

            //leemos la linea, y la dividimos
            string[] itemLoc = partidaGuardada.ReadLine().Split(' ');
            map.RestoreLocation(itemLoc); //restauramos localizaciones de los items con respecto a los datos del archivo

            partidaGuardada.Close(); //cerramos flujo

            Console.Clear(); //limpiamos pantalla
        }

        static void CommandFile(Player player, Map map, out bool quit) //metodo para elegir si se procesa o no una lista de comandos
        {
            quit = false;
            int opcion;
            do //pedimos input al usuario hasta que haya seleccionado una opcion valida (Si o No)
            {
                Console.WriteLine("Do you wanna use a file to do some commands in succesion?\n1. Yes\n2. No");
                try
                {
                    opcion = int.Parse(Console.ReadLine());
                }
                catch //si no escribe bien la opcion
                {
                    Console.Clear(); //obligamos a que se repita el bucle
                    Console.WriteLine("Write the answer in the correct format (1 or 2).");
                    opcion = -1;
                }
            } while (opcion < 1 || opcion > 2);
            if (opcion == 1) ReadCommandFile(player, map, out quit); //si ha elegido leer comando, procesamos la informacion
            else InfoPlace(map, player.GetPosition()); //imprimimos el primer estado del jugador (primera sala)
        }

        static void ReadCommandFile(Player player, Map map, out bool quit) //metodo para procesar la lista de comandos
        {
            quit = false;
            string file = "";
            bool validFile;
            do //pedimos el archivo a leer hasta que sea valido
            {
                Console.Write("Insert file (with extension): ");
                try
                {
                    file = Console.ReadLine();
                    if (!File.Exists(file)) throw new Exception("File doesn't exist.");
                    validFile = true;
                }
                catch (Exception e)
                {
                    //si no ha introducido un archivo correcto, forzamos el bucle
                    Console.WriteLine(e.Message);
                    validFile = false;
                }
            } while (!validFile);

            //imprimimos la informacion del lugar actual
            InfoPlace(map, player.GetPosition());

            //hacemos flujo de lectura
            StreamReader commands = new StreamReader(file);

            //leemos linea a linea hasta que se acabe el archivo, o se cumpla una de las tres condiciones de salida
            while (!commands.EndOfStream && !ArrivedAtExit(map, player) && player.IsAlive() && !quit)
            {
                string commandLine = commands.ReadLine();
                Console.WriteLine(">" + commandLine); //escribimos el comando
                HandleInput(commandLine, player, map, out quit); //procesamos el comando
            }

            commands.Close(); //cerramos flujo
        }

        static void FinalMessage(Player p, bool quit) //metodo que muestra el mensaje final de partida
        {
            if (quit) Console.WriteLine("You forfeited :(. See ya soon!"); //mensaje si ha salido
            else if (!p.IsAlive())
            {
                Console.WriteLine("Sadly, you died :(. Try again!"); //mensaje si ha muerto
                if (File.Exists("partidaGuardada.txt")) File.Delete("partidaGuardada.txt"); //eliminamos el archivo de guardado
            }
            else
            {
                Console.WriteLine("You escaped :). Good job!"); //mensaje si ha ganado
                if (File.Exists("partidaGuardada.txt")) File.Delete("partidaGuardada.txt"); //eliminamos el archivo de guardado
            }
        }

        static int GetDirection(string dir) //metodo para hallar la direccion establecida
        {
            int direction = -1;
            switch (dir)
            {
                case "n": //norte
                    direction = 0;
                    break;
                case "s": //sur
                    direction = 1;
                    break;
                case "e": //este
                    direction = 2;
                    break;
                case "w": //oeste
                    direction = 3;
                    break;
            }

            //devolvemos la direccion (-1 si no existe)
            return direction;
        }

        static string Commands() //metodo para obtener la lista de comandos
        {
            //devolvemos la lista completa de comandos
            return "go <direccion>: permite moverse en la direccion indicada (n, s, e, w)." +
                "\npick <item>: permite recoger el objeto indicado del lugar actual." +
                "\nlook: muestra los objetos que hay en lugar actual." +
                "\ninfo: muestra la información sobre el lugar actual y las direcciones hacia las que se puede mover" +
                "\ninventory: muestra información sobre tu inventario." +
                "\neat <item>: consume el objeto indicado del inventario (en caso de tener HP obtenible)" +
                "\ndrop <item>: permite colocar un objeto del inventario en la sala actual" +
                "\nme: muestra tu información." +
                "\nquit: permite finalizar el juego." +
                "\nhelp: muestra el repertorio de comandos." +
                "\nclear: limpia la informacion en pantalla";
        }

        static void InfoPlace(Map m, int roomNumber) //metodo que escribe la información de la sala actual
        {
            //imprimimos la informacion de la sala, y sus conexiones
            Console.WriteLine(m.GetRoomInfo(roomNumber) + "\n" + m.GetMovesInfo(roomNumber));
        }

        static bool ArrivedAtExit(Map m, Player p) //metodo que comprueba si se ha llegado a la salida
        {
            //devolvemos 'true' en caso de que la posicion actual sea la salida, 'false' en caso contrario
            return m.IsExit(p.GetPosition());
        }
    }
}
