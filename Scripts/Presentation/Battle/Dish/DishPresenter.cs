namespace Unity1week202412.Battle.Dish
{
    public class DishPresenter
    {
        private readonly DishSwitcher _dishSwitcher;

        public DishPresenter(DishSwitcher dishSwitcher)
        {
            _dishSwitcher = dishSwitcher;
        }

        public void SwitchDish(int dishId)
        {
            _dishSwitcher.Switch(dishId);
        }
    }
}