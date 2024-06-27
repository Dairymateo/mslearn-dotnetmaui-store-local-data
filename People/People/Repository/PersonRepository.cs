using People.Models;
using SQLite;

namespace People.Repository;

public class PersonRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection
    private SQLiteConnection conn;

    private void Init()
    {
        // TODO: Add code to initialize the repository
        if (conn != null)
            return;


        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Person>();
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewPerson(Person person)
    {
        
        try
        {
            Init();
            conn.Insert(person);
            StatusMessage = "Persona " + person.Name + "Guardad correctamente";
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", person.Name, ex.Message);
            throw;
        }

    }

    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            // TODO: Call Init()
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            // TODO: Insert the new person into the database
            result = conn.Insert(new Person { Name = name });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }


    public void UpdatePerson(Person person)
    {
        try
        {
            Init();
            conn.Update(person);
        }
        catch (Exception ex)
        {
            throw;
        
        }
    }

    public List<Person> GetAllPeople()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            Init();
            return conn.Table<Person>().ToList().OrderBy(p => p.Name).ToList();

        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Person>();
    }


    public Person GetPerson(int id)
    {
        try
        {
            Init();
            var people = conn.Table<Person>().ToList();
            Person person = people.Find(x => x.Id == id);
            return person;

        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new Person();


    }



    public void DeletePerson(Person person)
    {
        try
        {
            Init();
            conn.Delete(person);

        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }




    }
}
