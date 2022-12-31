using System;
using UnityEngine;

namespace FourPics
{
    public class PlayerPrefsStorage : IStorage
    {
        public bool HasKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return PlayerPrefs.HasKey(key);
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        public float GetFloat(string key, float defaultValue = 0f)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public string GetString(string key, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return PlayerPrefs.GetString(key, defaultValue);
        }

        public void SetBool(string key, bool value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public void SetFloat(string key, float value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            PlayerPrefs.SetFloat(key, value);
        }

        public void SetInt(string key, int value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            PlayerPrefs.SetInt(key, value);
        }

        public void SetString(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            PlayerPrefs.SetString(key, value);
        }

        public void Clear(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            PlayerPrefs.DeleteKey(key);
        }
    }
}
