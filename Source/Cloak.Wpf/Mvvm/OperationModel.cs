using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cloak.Wpf.Mvvm
{
	public class OperationModel : ViewModel
	{
		private OperationStatus _status;

		public OperationModel()
		{
			ErrorAcknowledgedCommand = new MethodCommand(OnErrorAcknowledged);
		}

		public OperationStatus Status
		{
			get { return _status; }
			private set { SetProperty("Status", ref _status, value); }
		}

		public bool IsInProgress
		{
			get { return _status == OperationStatus.InProgress; }
		}

		public ICommand ErrorAcknowledgedCommand { get; private set; }

		public async Task<TResult> ExecuteAsync<TResult>(Func<TResult> work)
		{
			Start();

			TResult result;
			Exception exception = null;

			try
			{
				result = await Task.Run(work);
			}
			catch(Exception ex)
			{
				result = default(TResult);

				exception = ex;
			}

			if(exception != null)
			{
				CompleteWithError(exception);
			}
			else
			{
				Complete();
			}

			return result;
		}

		private void Start()
		{
			TransitionStatus(OperationStatus.Idle, OperationStatus.InProgress);
		}

		private void Complete()
		{
			TransitionStatus(OperationStatus.InProgress, OperationStatus.Idle);
		}

		private void CompleteWithError(Exception exception)
		{
			// TODO: Do something with the exception

			TransitionStatus(OperationStatus.InProgress, OperationStatus.Error);
		}

		private void OnErrorAcknowledged()
		{
			TransitionStatus(OperationStatus.Error, OperationStatus.Idle);
		}

		private void TransitionStatus(OperationStatus validCurrentStatus, OperationStatus newStatus)
		{
			if(Status != validCurrentStatus)
			{
				throw new InvalidOperationException(Resources.InvalidOperationStatusUpdate.FormatInvariant(Status, newStatus, validCurrentStatus));
			}

			Status = newStatus;
		}

		protected override void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);

			if(e.PropertyName == "Status")
			{
				RaisePropertyChanged("IsInProgress");
			}
		}
	}
}