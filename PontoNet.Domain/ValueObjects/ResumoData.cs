namespace PontoNet.Domain.ValueObjects
{
    public class ResumoData
    {
        public DateTime Data { get; private set; }

        public TimeSpan RestanteData { get; private set; }

        public TimeSpan RestanteMes { get; private set; }

        public TimeSpan RestanteFinal { get; private set; }
        
        public ResumoData(DateTime data, TimeSpan restanteData, TimeSpan restanteMes, TimeSpan restanteFinal)
        {
            Data = data;
            RestanteData = restanteData;
            RestanteMes = restanteMes;
            RestanteFinal = restanteFinal;
        }
    }
}