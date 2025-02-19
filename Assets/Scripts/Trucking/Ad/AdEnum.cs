/*
 * Created on 2019년 3월 13일 수요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

namespace Trucking.Ad
{
    public enum AdUnit
    {
        Airplane_Cash,
        Job_Complete,
        Job_Refresh,
        Free_Cash,
        Free_Fuel,
        Delivery_Bosster,
        Time_Boost
    }

    public enum AdResult
    {
        /// <summary>
        /// 광고보기 성공
        /// </summary>
        Success,

        /// <summary>
        /// 광고 취소
        /// </summary>
        Cancle,

        /// <summary>
        /// 광고 없음
        /// </summary>
        NoFill,
    }
}