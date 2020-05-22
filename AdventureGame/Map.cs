using System;
using System.IO;
using Listas;

namespace Adventure
{
    public enum Direction { North, South, East, West };
    public class Map
    {
        //items
        public struct Item
        {
            public string name, description;
            public int hp; //health points
            public int weight; //peso del item
        }

        //lugares del mapa
        public struct Room
        {
            public string name, description;
            public bool exit; //es salida?
            public int[] connections; //vector de 4 componentes
                                      //con el lugar al norte, sur, este y oeste
                                      //-1 si no hay conexion
            public Lista itemsInRoom; //indices al vector de items n los items del lugar
        }

        Room[] rooms; //vector de lugares del mapa
        Item[] items; //vector de items del juego
        int nRooms, nItems; //numero de lugares y numero de items
        int entryRoom; //numero de la habitacion de entrada (leida del mapa)

        public Map(int numRooms, int numItems) //constructora del mapa
        {
            //inicializamos el número de habitaciones y objetos
            nRooms = 0;
            nItems = 0;

            //creamos los arrays de dichas habitaciones y objetos (sobredimensionados)
            rooms = new Room[numRooms];
            items = new Item[numItems];
        }

        public void ReadMap(string file) //metodo de inicializacion del mapa
        {
            //si el archivo no existe, lanzamos excepcion correspondiente
            if (!File.Exists(file)) throw new Exception("The file doesn't exist.");

            //iniciamos el lector de archivo
            StreamReader archivo = new StreamReader(file);
            //iniciamos el contador de lineas del archivo
            int lineaArchivo = 1;

            try
            {
                while (!archivo.EndOfStream) //mientras no se haya llegado al final del archivo
                {
                    //leemos la linea del archivo
                    string linea = archivo.ReadLine();

                    //si no es una linea vacía o comentada
                    if (!(linea.Trim() == "") && !linea.StartsWith("//"))
                    {
                        InsertInformation(linea); //procesamos la informacion
                    }

                    //aumentamos el contador de lineas del archivo
                    lineaArchivo++;
                }

                //cerramos flujo de archivo
                archivo.Close();
            }
            catch (Exception e) //en caso de hallar una excepcion
            {
                //lanzamos esa excepcion con la linea en la que ocurre
                throw new Exception(e.Message + " Line " + lineaArchivo);
            }
        }

        private void InsertInformation(string linea) //metodo para insertar la informacion dada en el archivo al mapa
        {
            if (linea.StartsWith("room")) //'room' implica crear una sala
            {
                CreateRoom(linea);
            }
            else if (linea.StartsWith("conn")) //'conn' implica estabalecer las conexiones de una sala
            {
                EstablishConnections(linea);
            }
            else if (linea.StartsWith("item")) //'item' implica crear un item
            {
                CreateItem(linea);
            }
            else if (linea.StartsWith("entry")) //'entry' implica establecer la sala de inicio
            {
                InsertEntryRoom(linea);
            }
            else if (linea.StartsWith("exit")) //'exit' implica establecer que sala es la salida
            {
                EstablishExit(linea);
            }
            else throw new Exception("Invalid command."); //si no es ninguno de estos, lanzamos excepcion
        }

        private void CreateRoom(string linea) //metodo para crear una sala en el mapa
        {
            //dividimos el comando
            string[] comando = linea.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int roomIndex = FindRoomByName(comando[1]); //buscamos si la sala ya existe

            if (roomIndex == -1) //si no existiese la sala previamente
            {
                if (nRooms < rooms.Length) //si la sala cupiese en el array de salas
                {
                    //inicializamos conexiones, guardamos descripcion, nombre, salida y itemsInRoom
                    rooms[nRooms].connections = new int[4];
                    InitializeConns(rooms[nRooms].connections);
                    rooms[nRooms].description = ReadDescription(linea);
                    rooms[nRooms].name = comando[1];
                    rooms[nRooms].exit = false;
                    rooms[nRooms].itemsInRoom = new Lista();
                    nRooms++; //aumentamos el contador de salas
                }
                else throw new Exception("There's no space for this room."); //en caso contrario, excepcion
            }
            else throw new Exception("The room already exists."); //en caso contrario, excepcion
        }

        private void EstablishConnections(string linea) //metodo para establecer la conexion entre dos salas
        {
            //dividimos el comando
            string[] comando = linea.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int dirIndex = GetDirection(comando[2]); //buscamos la direccion

            //si no existe esa direccion, lanzamos excepcion
            if (dirIndex == -1) throw new Exception("Invalid direction.");

            int roomIndex1 = FindRoomByName(comando[1]); //buscamos la sala A
            int roomIndex2 = FindRoomByName(comando[3]); //buscamos la sala B

            if (roomIndex1 != -1 && roomIndex2 != -1) //si ambos existen
            {
                rooms[roomIndex1].connections[dirIndex] = roomIndex2; //establecemos la conexion A -> B
                //establecemos la conexion B -> A
                if (dirIndex == 0 || dirIndex == 2) rooms[roomIndex2].connections[dirIndex + 1] = roomIndex1; //si dir = 'n' ó 'e'
                else rooms[roomIndex2].connections[dirIndex - 1] = roomIndex1; //si dir = 's' ó 'w'
            }
            else //si no existiese, lanzamos una excepcion dependiendo de quien no exista
            {
                //si son ambos
                if (roomIndex1 == -1 && roomIndex2 == -1) throw new Exception("Both origin and destination room don't exist.");
                else
                {
                    //si es la sala A
                    if (roomIndex1 == -1) throw new Exception("The origin room doesn't exist.");
                    throw new Exception("The destination room doesn't exist."); //si es la sala B
                }
            }
        }

        private void CreateItem(string linea) //metodo para crear un item en el mapa
        {
            //dividimos el comando
            string[] comando = linea.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int itemIndex = FindItemByName(comando[1]); //buscamos si el item ya existe

            if (itemIndex == -1) //si no existiese el item previamente
            {
                if (nItems < items.Length) //si cupiese en el array de items
                {
                    //guardamos descripcion, noombre, peso y HP
                    items[nItems].description = ReadDescription(linea);
                    items[nItems].name = comando[1];
                    items[nItems].weight = int.Parse(comando[2]);
                    items[nItems].hp = int.Parse(comando[3]);
                }
                else throw new Exception("There's no space for this item."); //en caso contrario, excepcion
            }
            else throw new Exception("The item already exists."); //en caso contrario, excepcion

            int roomIndex = FindRoomByName(comando[4]); //buscamos la habitacion en la que se encuentra el item

            if (roomIndex != -1) //si existiese
            {
                rooms[roomIndex].itemsInRoom.Inserta(nItems); //la insertamos en la sala
                nItems++; //aumentamos el contador de items
            }
            else throw new Exception("The room where the item should be placed doesn't exist."); //en caso contrario, excepcion
        }

        private void InsertEntryRoom(string linea) //metodo para establecer el comienzo de la partida (sala origen) 
        {
            //dividimos el comando
            string[] comando = linea.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int roomIndex = FindRoomByName(comando[1]); //buscamos la sala

            //si existe la sala, guardamos su indice como entrada
            if (roomIndex != -1) entryRoom = roomIndex;
            else throw new Exception("The name of the entry room doesn't exists."); //en caso contrario, excepcion
        }

        private void EstablishExit(string linea) //metodo para establecer la sala de salida
        {
            //dividimos el comando
            string[] comando = linea.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int roomIndex = FindRoomByName(comando[1]); //buscamos la sala

            //si existe la sala, la hacemos salida
            if (roomIndex != -1) rooms[roomIndex].exit = true;
            else throw new Exception("The name of the exit doesn't exists."); //en caso contrario, excepcion
        }

        private string ReadDescription(string linea) //metodo para devolver la descripcion de una sala o item
        {
            //devolvemos el string descripcion entrecomillado
            return "\"" + linea.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries)[1] + "\"";
        }

        private void InitializeConns(int[] conns) //metodo para inicializar las conexiones
        {
            for (int i = 0; i < conns.Length; i++) //recorremos las conexiones, inicializandolas a -1
            {
                conns[i] = -1;
            }
        }

        private int GetDirection(string dir) //metodo para traducir la direccion (string) a entero
        {
            int aux = -1; //entero de direccion

            switch (dir)
            {
                case "n": //si es norte
                    aux = 0;
                    break;
                case "s": //si es sur
                    aux = 1;
                    break;
                case "e": //si es este
                    aux = 2;
                    break;
                case "w": //si es oeste
                    aux = 3;
                    break;
            }

            //devolvemos la direccion
            return aux;
        }

        public int FindItemByName(string itemName) //metodo para buscar un item a traves de su nombre
        {
            int i = 0;
            while (i < nItems && items[i].name != itemName) //buscamos el item a lo largo del array
            {
                i++;
            }

            //si existe, devolvemos su indice
            if (i < nItems) return i;
            //si no, devolvemos -1
            return -1;
        }

        private int FindRoomByName(string roomName) //metodo para buscar una sala a traves de su nombre
        {
            int i = 0;
            while (i < nRooms && rooms[i].name != roomName) //buscamos la sala a lo largo del array
            {
                i++;
            }

            //si existe, devolvemos su indice
            if (i < nRooms) return i;
            //si no, devolvemos -1
            return -1;
        }

        public int GetItemWeight(int itemNumber) //metodo que devuelve el peso del item
        {
            //devuelve el atributo del peso del item
            return items[itemNumber].weight;
        }

        public int GetItemHP(int itemNumber) //metodo que devuelve el HP recuperable del item
        {
            //devuelve el atributo HP del item
            return items[itemNumber].hp;
        }

        public string PrintItemInfo(int itemNumber) //método que devuelve la informacion de un item de la sala
        {
            //devuelve el nombre, peso, hp que recupera y descripcion del item
            return "Item: " + items[itemNumber].name + " Weight: " + GetItemWeight(itemNumber) +
                " HP: " + GetItemHP(itemNumber) + " " + items[itemNumber].description + ".\n";
        }

        public string GetRoomInfo(int roomNumber) //metodo que devuelve la informaion de una sala
        {
            //devuelve el nombre de la sala, seguido de su descripcion
            return rooms[roomNumber].name + " " + rooms[roomNumber].description;
        }

        public string GetInfoItemsInRoom(int roomNumber) //metodo que devuelve la informacion de los items de una sala
        {
            string infoItems = ""; //string de items
            int items = rooms[roomNumber].itemsInRoom.CuentaElementos(); //contamos los elementos en la sala
            int i = 1; //1 porque N_Esimo empieza desde 1
            while (i <= items) //recorremos todos los elementos de la sala
            {
                infoItems += PrintItemInfo(rooms[roomNumber].itemsInRoom.N_Esimo(i)); //guardamos la informacion de cada item
                i++;
            }

            //si no hay ningun item, devolvemos el mensaje por defecto
            if (infoItems == "") return "I don’t see anything notable here.\n";
            return infoItems; //en caso contrario, devolvemos la informacion
        }

        public bool PickItemInRoom(int roomNumber, int itemNumber) //metodo para recoger un item de una sala del mapa
        {
            if (rooms[roomNumber].itemsInRoom.BuscaDato(itemNumber)) //si el item esta en la sala
            {
                rooms[roomNumber].itemsInRoom.BorraElemento(itemNumber); //lo eliminamos de la sala
                return true; //devolvemos true
            }

            //si no esta, devolvemos false
            return false;
        }

        public void DropItemInRoom(int roomNumber, int itemNumber) //metodo para soltar un item en una sala
        {
            //hacemos que el nuevo item esté entre los items de la sala
            rooms[roomNumber].itemsInRoom.Inserta(itemNumber);
        }

        public bool IsExit(int roomNumber) //metodo que comprueba si una sala es la salida
        {
            //devuelve atributo 'exit' de la sala
            return rooms[roomNumber].exit;
        }

        public int GetEntryRoom() //metodo que accede a la sala de entrada
        {
            //devuelve el indice de la sala de entrada
            return entryRoom;
        }

        public string GetMovesInfo(int roomNumber) //metodo que devuelve los posibles movimientos en una sala
        {
            string infoMoves = ""; //string de movimientos
            int i = 0;
            while (i < rooms[roomNumber].connections.Length) //recorremos el array de conexiones de la habitacion
            {
                if (rooms[roomNumber].connections[i] != -1) //si hubiese conexion, lo añadimos al string de movimientos
                {
                    infoMoves += ((Direction)i).ToString() + ": " + rooms[rooms[roomNumber].connections[i]].name + "\n";
                }
                i++;
            }

            //devuelve la informacion de los movimientos posibles
            return infoMoves;
        }

        public int Move(int roomNumber, Direction dir) //metodo que devuelve la sala a moverse
        {
            //devuelve el indice de la sala a moverse
            return rooms[roomNumber].connections[(int)dir];
        }

        public void SaveMap(StreamWriter guardado) //metodo para guardar la informacion necesaria en el archivo de guardado
        {
            guardado.WriteLine(SaveItemsInRoom()); //guardamos las localizaciones de los items
        }

        private string SaveItemsInRoom() //metodo para guardar las localizaciones de los items en el archivo de guardado
        {
            string items = ""; //string items

            for (int j = 0; j < nItems; j++) //para cada item
            {
                int r = 0;
                while (r < nRooms && !rooms[r].itemsInRoom.BuscaDato(j)) r++; //buscamos la sala en la que está

                //si esta en alguna sala, guardamos la localizacion y el item
                if (r != nRooms) items += r + " " + j + " ";
            }

            //devolvemos el string a guardar
            return items;
        }

        public void SearchDelete(int item) //metodo de eliminacion de un item en una sala al restaurar inventario
        {
            int i = 0;
            while (!rooms[i].itemsInRoom.BuscaDato(item)) i++; //buscamos la habitacion en una sala

            rooms[i].itemsInRoom.BorraElemento(item); //la eliminamos
        }

        public void RestoreLocation(string[] itemLocs) //metodo para restaurar las posiciones de los items de la partida de guardado
        {
            for (int i = 0; i < nItems; i++) //para cada item
            {
                int j = 0;
                while (j < nRooms && !rooms[j].itemsInRoom.BuscaDato(i)) j++; //buscamos el item en las salas

                if (j != nRooms) //si esta en alguna (no lo eliminamos cuando creamos inventario)
                {
                    int k = 1;
                    while (k < itemLocs.Length && int.Parse(itemLocs[k]) != i) k += 2; //lo buscamos en la lista de items del archivo

                    if (k < itemLocs.Length) //si esta en la lista
                    {
                        if (int.Parse(itemLocs[k - 1]) != j) //si no está en la misma sala que el mapa original
                        {
                            rooms[j].itemsInRoom.BorraElemento(int.Parse(itemLocs[k])); //lo eliminamos de la sala original
                            //lo insertamos en la sala guardada
                            rooms[int.Parse(itemLocs[k - 1])].itemsInRoom.Inserta(int.Parse(itemLocs[k]));
                        }
                    }
                    //si no esta en el inventario ni en el archivo, es que fue ingerido, por lo que lo eliminamos
                    else rooms[j].itemsInRoom.BorraElemento(j);
                }
            }
        }

        #region MetodosTestsMap
        public void CreateMap(int numHabs, int numItems, int[] localizations, int exitRoom, int entryRoomIndex)
        {
            //bucle de creación de habitaciones
            for (int i = 0; i < numHabs; i++)
            {
                rooms[nRooms].description = "Esta es la sala " + nRooms;
                rooms[nRooms].connections = new int[4];
                InitializeConns(rooms[nRooms].connections);
                rooms[nRooms].name = "Sala " + nRooms;
                rooms[nRooms].exit = false;
                rooms[nRooms].itemsInRoom = new Lista();
                nRooms++;
            }

            //bucle de creación de items
            for (int i = 0; i < numItems; i++)
            {
                items[nItems].description = "Este es el item " + nItems;
                items[nItems].name = "Item " + nItems;
                items[nItems].weight = nItems + 1;
                items[nItems].hp = nItems + 2;
                rooms[localizations[nItems]].itemsInRoom.Inserta(nItems);
                nItems++;
            }

            //bucle para establecer conexiones
            for (int i = 0; i < nRooms - 1; i++)
            {
                if (i <= 2)
                {
                    rooms[i].connections[0] = i + 1;
                    rooms[i + 1].connections[1] = i;
                }
                else
                {
                    rooms[i].connections[1] = i + 1;
                    rooms[i + 1].connections[0] = i;
                }
            }
            //concretamos algunas conexiones especiales
            rooms[1].connections[2] = 4;
            rooms[3].connections[3] = 2;
            rooms[2].connections[0] = -1;
            rooms[5].connections[1] = -1;

            entryRoom = entryRoomIndex;
            rooms[exitRoom].exit = true;
        }

        //metodo auxiliar para tests de unidad que devuelve el numero de elementos en la sala introducida
        public int GetNumeroElementosSala(int sala)
        {
            return rooms[sala].itemsInRoom.CuentaElementos();
        }
        #endregion
    }
}