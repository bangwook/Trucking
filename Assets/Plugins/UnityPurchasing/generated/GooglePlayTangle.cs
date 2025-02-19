#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("YpIdwOBiyYtWqC8INO9c3PEdXrmRduhe4Sy22ucpkKV5SbLbir1PAGhPGhcv+eYydF3hFOAZ9jqfJixqszA+MQGzMDszszAwMeJKQ1IZQs6y24q9TwAPbCwzMjAxMAGzMBMBPKUx8ooQeupOjgREir6cHEWqg26pidUlQWV5HL039shAShyVqJhb6yYWIY10a8h5KbtsXCm5bYIK45ZCcVgLCKlHWYJ8078vwEBN+xmxcnMipR+QT7pXkXboXuEsttrnKZCleUkPXUCsMfbd/VTtHECvdqUx8ooQehXXc029Gts9VIn76CAD2llkbJ4AlaiYW+smNovo39y2D25aN0iIqT1c3PEdXrkPXUCsMfbd/VTtHECvdupOjgREir6cHEWqg26paE8aFy/5MDszszAwMeJKQ1IZQs4WIY10a8iCfNO/L8BATfsZsXJzIqUfkE+6V1r2419CFoS/0GoLgJjFFddzTb0aNzgbt3m3xjwwMDA0MTKzMD4xAbNq+eNYXIxa9uNfQhaEv9BqC4CYxYXMSUSV2kv7INXa6xUfzEwAGGfD2z1UifvoIAPaWWRsngCFzElEldo2i+jf3LYPblo3SIipPZE0a/gZ35E0a/gZ32KSHcDgYsmLVqgvCDTvmvN84Wc2idUlQWV5HL039shAShzmMnRd4RTgGfY6nyYsamr541hcjHkpu2xcKbltggrjlkJxmvN84Wc2AbMwEwE8Nzgbt3m3xjwwMDA0MTJL+yDV2usVH8xMABhnw1gLCKlHWQ9sLDMyMDEw");
        private static int[] order = new int[] { 20,28,8,28,14,7,18,8,12,13,21,21,14,18,22,16,27,24,27,27,21,25,27,27,28,27,28,27,28,29 };
        private static int key = 49;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
