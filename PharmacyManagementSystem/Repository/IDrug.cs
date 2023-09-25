using PharmacyManagementSystem.Model;

namespace PharmacyManagementSystem.Repository
{
    public interface IDrug
    {
        void Add(Drug drug);
        void Update(Drug drug);
        Drug GetDrug(string drug);
        Drug GetDrugList(string DrugName);
        void Delete(int id);
        List<Drug> GetDrugList();
    }
}
