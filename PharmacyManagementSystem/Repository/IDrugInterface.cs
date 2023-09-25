using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Model;

namespace PharmacyManagementSystem.Repository
{
    public class IDrugInterface : IDrug
    {
        private readonly DataContext _context;

        public IDrugInterface(DataContext context)
        {
            _context = context;
        }

        public void Add(Drug drug)
        {
            _context.Drugs.Add(drug);
            _context.SaveChanges();
        }

        public void Update(Drug drug)
        {
            _context.Drugs.Update(drug);
            _context.SaveChanges();
        }

        public Drug GetDrug(string drugName)
        {
            return _context.Drugs.FirstOrDefault(d => d.DrugName == drugName);
        }

        public Drug GetDrugList(string DrugName)
        {
            return _context.Drugs.FirstOrDefault();
        }

        public void Delete(int id)
        {
            var drug = _context.Drugs.Find(id);
            if (drug != null)
            {
                _context.Drugs.Remove(drug);
                _context.SaveChanges();
            }
        }
        public List<Drug> GetDrugList()
        {
            return _context.Drugs.ToList();
        }
    }
}