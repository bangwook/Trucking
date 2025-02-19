using TMPro;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.Popup
{
    public class Popup_CityUpgrade : Popup_Base<Popup_CityUpgrade>
    {
        public Button btnTooltip;
        public Transform trsIconImage;
        public TextMeshProUGUI txtCityName;

        public TextMeshProUGUI txtCargoCount;
        public TextMeshProUGUI txtCargoCount2;
        public TextMeshProUGUI txtCargoCount3;

//        public TextMeshProUGUI txtCash;
        public TextMeshProUGUI txtGold;
//        public TextMeshProUGUI txtVoucher;

        public Button btnConfirm;

        public Image imgBtnConfirmGray;
//        public Button btnCashConfirm;

        public GameObject upgradeGroup;
        public GameObject maxGroup;

        public Image imgCity;
        public ParticleSystem particle;

        private ReactiveProperty<City> city = new ReactiveProperty<City>();
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        private void Start()
        {
            btnConfirm.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (UserDataManager.Instance.UseResource(
                        Datas.joblistExpansion[city.Value.model.level.Value + 1].gold,
                        0))
                    {
                        AudioManager.Instance.PlaySound("sfx_upgrade");

                        particle.gameObject.SetActive(true);
                        particle.Stop();
                        particle.Clear();
                        particle.Play();
                        city.Value.Upgrade();
                        FBAnalytics.FBAnalytics.LogUpgradeCityEvent(UserDataManager.Instance.data.lv.Value,
                            city.Value.name, city.Value.model.level.Value + 1,
                            "coin");
                        GetComponent<Animator>().Play("popup_city_upgrade", -1, 0);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                    }
                })
                .AddTo(this);

            btnTooltip.OnClickAsObservable().Subscribe(x => { Popup_GuideMain.Instance.Show(8); }).AddTo(this);

            city.Subscribe(ct =>
            {
                _compositeDisposable.Clear();

                if (ct != null)
                {
                    txtCityName.text = Utilities.GetStringByData(ct.data.string_name);

                    ct.model.level.Subscribe(lv =>
                    {
                        ct.GetComponent<MeshFilter>().sharedMesh = City.GetCityMesh(ct);

                        for (int i = 0; i < trsIconImage.childCount; i++)
                        {
                            trsIconImage.GetChild(i).gameObject.SetActive(i == Datas.joblistExpansion[lv].icon - 1);
                        }

                        if (ct.data.mega)
                        {
                            imgCity.sprite = GameManager.Instance.atlasCity.GetSprite("city_" + ct.data.id);
                        }
                        else
                        {
                            string meshName = ct.GetComponent<MeshFilter>().sharedMesh.name;
                            string strType = meshName.Substring(meshName.Length - 1).ToLower();
                            imgCity.sprite =
                                GameManager.Instance.atlasCity.GetSprite(
                                    "city_" + strType + "_00" + Datas.joblistExpansion[lv].city_img);
                        }

                        imgCity.SetNativeSize();


                        btnConfirm.gameObject.SetActive(lv < Datas.joblistExpansion.Length - 1);
//                        btnCashConfirm.gameObject.SetActive(lv < Datas.joblistExpansion.Length - 1);

                        maxGroup.SetActive(lv >= Datas.joblistExpansion.Length - 1);
                        upgradeGroup.SetActive(lv < Datas.joblistExpansion.Length - 1);

                        if (lv < Datas.joblistExpansion.Length - 1)
                        {
                            txtCargoCount.text = Datas.joblistExpansion[lv].max_numb.ToString();
                            txtCargoCount2.text = Datas.joblistExpansion[lv + 1].max_numb.ToString();

//                            UserDataManager.Instance.data.cash.Subscribe(cash =>
//                            {
//                                txtCash.text = Utilities.GetThousandCommaText(Datas.joblistExpansion[lv + 1].cash);
//                                txtCash.color = Color.white;
//                                if (cash < Datas.joblistExpansion[lv + 1].cash)
//                                {
//                                    txtCash.color = Color.red;
//                                }    
//                            }).AddTo(_compositeDisposable);

                            UserDataManager.Instance.data.gold.Subscribe(gold =>
                            {
                                txtGold.text = Utilities.GetThousandCommaText(Datas.joblistExpansion[lv + 1].gold);
                                txtGold.color = Color.white;
                                if (gold < Datas.joblistExpansion[lv + 1].gold)
                                {
                                    txtGold.color = Color.red;
                                }

                                imgBtnConfirmGray.gameObject.SetActive(gold < Datas.joblistExpansion[lv + 1].gold);
                            }).AddTo(_compositeDisposable);
                        }
                        else
                        {
                            txtCargoCount3.text = Datas.joblistExpansion[lv].max_numb.ToString();
                        }
                    }).AddTo(_compositeDisposable);
                }
            }).AddTo(this);
        }

        public void Show(City _city)
        {
            base.Show();
            particle.gameObject.SetActive(false);
            city.Value = _city;
            GetComponent<Animator>().Play("popup_truck_upgrade_idle", -1, 0);
        }

        public override void Close()
        {
            if (particle.isPlaying)
            {
                particle.Stop();
                particle.Clear();
            }

            city.Value = null;
            base.Close();
        }

        public override void BackKey()
        {
            GameManager.Instance.fsm.PopState();
        }
    }
}