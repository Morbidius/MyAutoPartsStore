namespace MyAutoPartsStore.Services
{
    public class Guard
    {
        public bool AgainstNull(object value, string name = null)
        {
            if (value == null)
            {
                name ??= "Value";
                return true;
            }
            return false;
        }
    }
}
