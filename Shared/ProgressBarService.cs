using System;

namespace omnia_blazor_demo.Shared
{
    public class ProgressBarService
    {
        public event Action OnShow;
		public event Action OnHide;

		public void Show()
		{
			OnShow?.Invoke();
		}

		public void Hide()
		{
			OnHide?.Invoke();
		}
    }
}