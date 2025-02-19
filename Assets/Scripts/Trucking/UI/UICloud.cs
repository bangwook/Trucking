using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UICloud : MonoBehaviour
    {
        public TextMeshProUGUI txtCity;
        public TextMeshProUGUI txtGold;
        public Button btnOpen;
        public GameObject btnOff;
        public Image productIcon;

        private int areaIndex;
        private long cost;
        private City city;
        private ReactiveProperty<bool> pre_unlock = new ReactiveProperty<bool>();

        public void Init(int index)
        {
            areaIndex = index;
            int area_open = index + 2;
            LevelData lvData = Datas.levelData.ToArray().FirstOrDefault(x => x.area_open == area_open);
            cost = lvData.area_open_cost;
            txtGold.text = Utilities.GetThousandCommaText(cost);
            city = GameManager.Instance.cities.FirstOrDefault(x => x.IsMega() && x.GetCloudArea() == area_open);
            txtCity.text = city.data.name;
            productIcon.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.parts, city.partIndex);

            if (lvData.pre_unlock[0] > 0)
            {
                List<IObservable<bool>> list = new List<IObservable<bool>>();

                for (int i = 0; i < lvData.pre_unlock.Length; i++)
                {
                    list.Add(UserDataManager.Instance.data.cloudOpen[lvData.pre_unlock[i] - 2]);
                }

                Observable.CombineLatest(list).Subscribe(lst =>
                {
                    bool result = false;

                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (lst[i])
                        {
                            result = true;
                            break;
                        }
                    }

                    pre_unlock.Value = result;
                }).AddTo(this);
            }
            else
            {
                pre_unlock.Value = true;
            }

            Observable.CombineLatest(pre_unlock,
                    UserDataManager.Instance.data.gold,
                    (unlock, gold) => (unlock, gold))
                .Subscribe(value =>
                {
                    btnOff.SetActive(value.Item2 < cost
                                     || !value.Item1);
                }).AddTo(this);

            btnOpen.OnClickAsObservable().Subscribe(_ =>
            {
                if (pre_unlock.Value && UserDataManager.Instance.UseGold(cost))
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    UserDataManager.Instance.data.cloudOpen[areaIndex].Value = true;
                }
                else
                {
                    AudioManager.Instance.PlaySound("sfx_require");
                }
            }).AddTo(this);

            UserDataManager.Instance.data.cloudOpen[areaIndex].Subscribe(open => { gameObject.SetActive(!open); })
                .AddTo(this);
        }

        private void LateUpdate()
        {
            if (Camera.main != null)
            {
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}