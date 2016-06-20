using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Data
{
    public class PeopleRepository
    {
        private string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetPeople()
        {
            using (PeopleDbDataContext context = new PeopleDbDataContext(_connectionString))
            {
                return context.Persons.ToList();
            }
        }

        public void AddPerson(Person person)
        {
            using (PeopleDbDataContext context = new PeopleDbDataContext(_connectionString))
            {
                context.Persons.InsertOnSubmit(person);
                context.SubmitChanges();
            }
        }

        public Person GetById(int id)
        {
            using (PeopleDbDataContext context = new PeopleDbDataContext(_connectionString))
            {
                return context.Persons.First(p => p.Id == id);
            }
        }

        public void UpdatePerson(Person person)
        {
            using (PeopleDbDataContext context = new PeopleDbDataContext(_connectionString))
            {
                context.Persons.Attach(person);
                context.Refresh(RefreshMode.KeepCurrentValues, person);
                context.SubmitChanges();
            }
        }

        public void DeletePerson(int id)
        {
            using (PeopleDbDataContext context = new PeopleDbDataContext(_connectionString))
            {
                context.ExecuteCommand("DELETE FROM People WHERE Id = {0}", id);
            }
        }
    }
}
