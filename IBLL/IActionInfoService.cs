using Model;

namespace IBLL
{
    public partial interface IActionInfoService
    {
        bool EditAction(ActionInfo actionInfo);
        bool DeleteActionById(int id);
    }
}