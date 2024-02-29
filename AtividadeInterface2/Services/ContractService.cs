using AtividadeInterface2.Entities;


namespace AtividadeInterface2.Services
{
    internal class ContractService
    {
        private IOnlinePaymentService _OnlinePaymentService;

        public ContractService(IOnlinePaymentService OnlinePaymentService)
        {
            _OnlinePaymentService = OnlinePaymentService;
        }

        public void ProcessContract(Contract contract, int months)
        {
            double BasicQuota = contract.TotalValue / months;

            for (int i = 1; i <= months; i++)
            {
                DateTime date = contract.Date.AddMonths(i);

                double UpdatedQuota = BasicQuota + _OnlinePaymentService.Interest(BasicQuota, i);

                double FullQuota = UpdatedQuota + _OnlinePaymentService.PaymentFee(UpdatedQuota);

                contract.AddInstallment(new Installment(date, FullQuota));
            }
        }
    }
}
