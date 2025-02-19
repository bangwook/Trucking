using System;
using System.Linq;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.Popup
{
    public class Popup_PartsUpgrade : Popup_Base<Popup_PartsUpgrade>
    {
        public TextMeshProUGUI txtTitle;

        public Image imgPart_L;
        public Image imgPart_R;
        public TextMeshProUGUI txtPart_L;
        public TextMeshProUGUI txtPart_R;
        public TextMeshProUGUI txtPartTime_L;
        public TextMeshProUGUI txtPartTime_R;
        public Image imgMaterial_L;
        public Image imgMaterial_R;
        public TextMeshProUGUI txtMaterial_L;
        public TextMeshProUGUI txtMaterial_R;

        public TextMeshProUGUI txtGold;
        public TextMeshProUGUI txtTime;

        public Button btnUpgrade;
        public Button btnUpgrade_gray;
        public Image imgCity;

        private ReactiveProperty<City> city = new ReactiveProperty<City>();
        private CompositeDisposable disposable = new CompositeDisposable();
        private PartsProduction data;
        private int partIndex;
        private int matIndex;

        private void Start()
        {
            btnUpgrade.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (UserDataManager.Instance.UseGold(data.up_gold[city.Value.model.productLevel.Value + 1]))
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");
                        city.Value.model.state.Value = CityModel.State.Upgrade;
                        city.Value.model.productTime.Value =
                            DateTime.Now.AddSeconds(data.up_time[city.Value.model.productLevel.Value + 1]);

                        if (LunarConsoleVariables.isCraft)
                        {
                            city.Value.model.productTime.Value = DateTime.Now.AddSeconds(10);
                        }

                        UserDataManager.Instance.SaveData();
                        GameManager.Instance.fsm.PopState();
                    }
                })
                .AddTo(this);

            btnUpgrade_gray.OnClickAsObservable()
                .Subscribe(_ => { AudioManager.Instance.PlaySound("sfx_require"); })
                .AddTo(this);

            city.Subscribe(ct =>
            {
                disposable.Clear();

                if (ct != null)
                {
                    ct.model.productLevel.Subscribe(lv =>
                    {
                        txtTitle.text = string.Format(Utilities.GetStringByData(20133), ct.name,
                            (lv + 1) + " > " + (lv + 2));
                        imgCity.sprite = GameManager.Instance.atlasCity.GetSprite("city_" + ct.data.id);
                        imgCity.SetNativeSize();

                        imgPart_L.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.parts, partIndex);
                        imgPart_R.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.parts, partIndex);
                        txtPart_L.text = "x" + data.parts_count[lv];
                        txtPart_R.text = "x" + data.parts_count[lv + 1];
                        txtPartTime_L.text = Utilities.GetTimeStringShort(TimeSpan.FromSeconds(data.pd_time[lv]));
                        txtPartTime_R.text = Utilities.GetTimeStringShort(TimeSpan.FromSeconds(data.pd_time[lv + 1]));
                        imgMaterial_L.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.material, matIndex);
                        imgMaterial_R.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.material, matIndex);
                        txtTime.text = Utilities.GetTimeStringShort(TimeSpan.FromSeconds(data.up_time[lv + 1]));
                        txtGold.text = Utilities.GetThousandCommaText(data.up_gold[lv + 1]);
                        txtMaterial_L.text = Utilities.GetThousandCommaText(data.material_count[lv]);
                        txtMaterial_R.text = Utilities.GetThousandCommaText(data.material_count[lv + 1]);
                    }).AddTo(disposable);

                    Observable.CombineLatest(ct.model.productLevel,
                        UserDataManager.Instance.data.gold,
                        (lv, gold) => gold >= data.up_gold[lv + 1]).Subscribe(value =>
                    {
                        txtGold.color = value ? Utilities.GetColorByHtmlString("#464646") : Color.red;
                        btnUpgrade.gameObject.SetActive(value);
                        btnUpgrade_gray.gameObject.SetActive(!value);
                    }).AddTo(disposable);
                }
            }).AddTo(this);
        }

        public void Show(City _city)
        {
            base.Show();
            data = Datas.partsProduction.ToArray().FirstOrDefault(x => x.city == _city.data.id);
            partIndex = RewardModel.GetIndex(RewardData.eType.parts, data.output);
            matIndex = RewardModel.GetIndex(RewardData.eType.material, data.input);
            city.Value = _city;
        }

        public override void Close()
        {
            city.Value = null;
            base.Close();
        }
    }
}