using System;
using System.IO;
using Listas;

namespace Adventure
{
    //publico para tests de unidad
    public class Player
    {
        string name; //nombre del jugador
        int pos; //lugar en el que se encuentra
        int hp; //numero de HP
        int weight; //peso de los objetos del inventario
        //este valor es publico para TESTEO DE CONSTRUCTORA sin depender de Map en GetInventoryInfo()
        public Lista inventory; //lista de objetos en el inventario

        const int MAX_HP = 10; //numero maximo de HP
        const int HP_PER_MOVEMENT = 2; //HP consumido por movimiento
        const int MAX_WEIGHT = 20; //maximo peso que puede llevar en el inventario

        public Player(string playerName, int entryRoom) //constructora de la clase
        {
            name = playerName; //asignamos el nombre
            pos = entryRoom; //establecemos la posición del jugador a la habitación de entrada
            hp = MAX_HP; //establecemos el HP al máximo
            weight = 0; //establecemos el peso a 0
            inventory = new Lista(); //creamos la lista 'inventory'
        }

        public Player(string playerName, int actualRoom, int hpSaved, int weightSaved) //constructora de la clase para reestablecer partida
        {
            name = playerName; //asignamos el nombre
            pos = actualRoom; //establecemos la posición del jugador a la habitación de entrada
            hp = hpSaved; //establecemos el HP al guardado
            weight = weightSaved; //establecemos el peso al guardado
            inventory = new Lista(); //creamos la lista 'inventory'
        }

        public int GetPosition() //método que devuelve la posición del jugador
        {
            //devuelve el índice de la sala en la que se encuentra el jugador
            return pos;
        }

        public bool IsAlive() //método que compruba si el jugador está vivo
        {
            //devuelve 'true' si el jugador sigue vivo, 'false' en caso contrario
            return hp > 0;
        }

        public bool Move(Map m, Direction dir) //método que mueve al jugador acorde a una direccion dada
        {
            int move = m.Move(pos, dir); //hallamos la sala a moverse

            //en caso de existir conexión en esa dirección
            if (move != -1)
            {
                pos = move; //actualizamos la posicion del jugador
                hp -= HP_PER_MOVEMENT; //restamos el HP acorde al movimiento
                return true; //Devolvemos 'true'
            }

            //en caso contrario, devolvemos 'false'
            return false;
        }

        public void PickItem(Map m, string itemName) //metodo para recoger un item de una sala
        {
            int item = m.FindItemByName(itemName); //buscamos el item en el mapa

            //si no existiese, lanzamos expecion
            if (item == -1) throw new Exception("Item doesn't exist.");

            //obtenemos su peso
            int itemWeight = m.GetItemWeight(item);

            //si el peso no excede el maximo permitido
            if (itemWeight + weight <= MAX_WEIGHT)
            {
                //si se puede coger el item porque se encuentra en la sala
                if (m.PickItemInRoom(pos, item))
                {
                    inventory.Inserta(item); //metemos el item en el inventario
                    weight += itemWeight; //aumentamos el peso del inventario
                }
                else throw new Exception("The item isn't in this room."); //en caso contrario, lanzmaos excepcion
            }
            else throw new Exception("The item surpasses the max weight of the inventory."); //en caso contrario, lanzmaos excepcion
        }

        public void EatItem(Map m, string itemName) //metodo para ingerir unitem del inventario
        {
            //buscamos el item
            int item = m.FindItemByName(itemName);

            //en caso de que no existiese, lanzamos excepcion
            if (item == -1) throw new Exception("Item doesn't exist.");

            //en caso de aue no se encuentre en el inventario, lanzamos excepcion
            if (!inventory.BuscaDato(item)) throw new Exception("The item isn't in the inventory.");

            int itemHP = m.GetItemHP(item); //obtenemos el HP del item

            //si el item no es ingerible, lanzamos excepcion
            if (itemHP == 0) throw new Exception("The item can't be eaten.");

            //en caso contrario
            inventory.BorraElemento(item); //eliminamos el item del inventario
            hp += itemHP; //aumentamos el HP
            weight -= m.GetItemWeight(item); //quitamos peso
            if (hp > MAX_HP) hp = MAX_HP; //en caso de exceder el maximo de HP, lo ajustamos
        }

        public void DropItem(Map m, string itemName) //metodo para soltar un item en una sala
        {
            int item = m.FindItemByName(itemName); //buscamos el item

            //si no existiese, lanzamos excepcion
            if (item == -1) throw new Exception("Item doesn't exist.");

            //si el item no se encuentra en el inventario, lanzamos excepcion
            if (!inventory.BuscaDato(item)) throw new Exception("The item isn't in the inventory.");

            //soltamos el item en la sala
            m.DropItemInRoom(pos, item);
            inventory.BorraElemento(item); //borramos el elemento del inventario
            weight -= m.GetItemWeight(item); //eliminamos el peso del item
        }

        public string GetInventoryInfo(Map m) //método que devuelve la informacion de los items en el inventario
        {
            string inventario = ""; //string de items
            int elemInventario = inventory.CuentaElementos(); //obtenemos el numero de elementos en el inventario
            int i = 1; //1 porque N_Esimo empieza desde 1
            while (i <= elemInventario) //recorremos el inventario
            {
                inventario += m.PrintItemInfo(inventory.N_Esimo(i)); //guardamos la informacion de cada item
                i++;
            }

            //de no haber items, devolvemos el mensaje por defecto
            if (inventario == "") return "My bag is empty.\n";
            return inventario; //en caso contrario, devolvemos la informacion
        }

        public string GetPlayerInfo() //metodo que devuelve la informacion del jugador
        {
            //devolvemos un string con el nombre, los health points y el peso de los items del inventario
            return "Name: " + name + " HP: " + hp + " Inventory weight: " + weight;
        }

        public void SavePlayer(StreamWriter guardado) //metodo para guardar la informacion del jugador en la partida de guardado
        {
            //guardamos nombre, posicion actual (indice), HP y peso del inventario
            guardado.WriteLine(name + " " + pos + " " + hp + " " + weight);
            guardado.WriteLine(SaveInventory()); //guardamos los items del inventario (indices)
        }

        private string SaveInventory() //metodo para guardar los items del inventario en la partida de guardado
        {
            string inventario = ""; //string inventario
            int elementos = inventory.CuentaElementos(); //contamos sus elementos
            int i = 1; //1 porque N_Esimo empieza desde 1
            while (i <= elementos) //recorremos el inventario
            {
                inventario += inventory.N_Esimo(i) + ((i + 1) > elementos ? "" : " "); //guardamos sus indices
                i++;
            }

            //devlvemos la informacion
            return inventario;
        }

        public void RestoreInventory(string[] items, Map map) //metodo para reestablecer inventario al restaurar partida
        {
            for (int i = 0; i < items.Length; i++) //para cada elemento en items
            {
                map.SearchDelete(int.Parse(items[i])); //lo eliminamos del mapa original
                inventory.Inserta(int.Parse(items[i])); //lo insertamos en el inventario
            }
        }
    }
}