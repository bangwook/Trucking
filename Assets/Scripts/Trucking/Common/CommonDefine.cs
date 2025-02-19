/*
 * Created on 2019년 3월 19일 화요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

namespace Trucking.Common
{
    public static class CommonDefine
    {
#if UNITY_IOS
    public const string Platform = "ios";
    public const string AppLink = "https://itunes.apple.com/app/id1434654648";
#elif UNITY_STANDALONE_OSX
    public const string Platform = "osx";
    public const string AppLink = "https://itunes.apple.com/app/id1434654648";
#elif PLATFORM_AMAZON
    public const string Platform = "amz";
    public const string AppLink =
 "https://play.google.com/store/apps/details?id=cookappsplay.pocket.truck.tycoon.idle.manage.game";
#else
        public const string Platform = "aos";
        public const string AppLink =
            "https://play.google.com/store/apps/details?id=cookappsplay.pocket.truck.tycoon.idle.manage.game";
#endif
    }
}