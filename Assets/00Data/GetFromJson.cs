using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GetFromJson 
{
    private Container contenedor;
   
    public List<DataJson> studentsList()
    {
        string ruta = Path.Combine(Application.streamingAssetsPath, "estudiantesData.json");

        string texto = File.ReadAllText(ruta);

        contenedor = JsonUtility.FromJson<Container>(texto);

        return contenedor.estudiantes;
    }
  
    public void SaveDataJsonFile(List<DataJson> newList)
    {
        Container con = new Container();
        con.estudiantes = newList;
        string textToJson = JsonUtility.ToJson(con);
        
        string ruta = Path.Combine(Application.streamingAssetsPath, "estudiantesData.json");
        File.WriteAllText(ruta,textToJson);
    }
    
    [System.Serializable]
    public class Container
    {
        public List<DataJson> estudiantes;
    }
    
}

[System.Serializable]
public class DataJson
{
    public string nombre;
    public string apellido;
    public string correo;
    public int codigo;
    public float nota;
    public string aprobado;
    public bool calificado;
}
