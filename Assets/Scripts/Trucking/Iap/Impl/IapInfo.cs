using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Trucking.Iap
{
    public interface IIapInfo
    {
        IapType Type { get; }
    }

    /// <summary>
    /// 결제 결과 콜백
    /// </summary>
    /// <param name="result"></param>
    public delegate void IapResult(bool result);

    /// <summary>
    /// 성공시 콜백
    /// </summary>
    /// <param name="id"></param>
    public delegate void IapSuccess(string id);

    /// <summary>
    /// 별도의 검증 결과
    /// </summary>
    /// <param name="product"></param>
    /// <param name="result"></param>
    public delegate void VerifyPeceiptResultOfficial(Product product, bool result);

    /// <summary>
    /// 별도의 검증
    /// </summary>
    /// <param name="product"></param>
    /// <param name="result"></param>
    public delegate void VerifyPeceiptOfficial(PurchaseEventArgs e, Product product, VerifyPeceiptResultOfficial result);

    /// <summary>
    /// unity가 플렛폼 결제 모듈
    /// </summary>
    public class IapInfoOfficial : IIapInfo
    {
        public class Product
        {
            public string id;
            public ProductType type;
            public IDs ids;
        }

        public IapType Type { get { return IapType.Official; } }

        /// <summary>
        /// 상품
        /// </summary>
        public IEnumerable<Product> Products;

        /// <summary>
        /// 별도의 검증(null이면 별도 검증없음)
        /// </summary>
        public VerifyPeceiptOfficial VerifyPeceipt;

        /// <summary>
        /// 성공시 콜백
        /// </summary>
        public IapSuccess Success;

        /// <summary>
        /// 아마존 센드박스 모드인가(테스트 결제)
        /// </summary>
        public bool AmazonSandboxMode = false;
    }

    public class Receipt
    {

        public string Store;
        public string TransactionID;
        public string Payload;

        public Receipt()
        {
            Store = TransactionID = Payload = "";
        }

        public Receipt(string store, string transactionID, string payload)
        {
            Store = store;
            TransactionID = transactionID;
            Payload = payload;
        }
    }

    public class PayloadAndroid
    {
        public string json;
        public string signature;

        public PayloadAndroid()
        {
            json = signature = "";
        }

        public PayloadAndroid(string _json, string _signature)
        {
            json = _json;
            signature = _signature;
        }
    }
}