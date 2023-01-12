using System;
using System.Collections;
using Source.Infrastructure;
using UnityEngine;

namespace Source.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IStorage : IService
    {
        string GetDataName();
        void SetFloat(string key, float value);
        float GetFloat(string key);
        bool HasKeyFloat(string key);
        void SetInt(string key, int value);
        int GetInt(string key);
        bool HasKeyInt(string key);
        void SetString(string key, string value);
        string GetString(string key);
        bool HasKeyString(string key);
        void SetVector3(string key, Vector3 value);
        Vector3 GetVector3(string key);
        bool HasKeyVector3(string key);
        void SetQuaternion(string key, Quaternion value);
        Quaternion GetQuaternion(string key);
        bool HasKeyQuaternion(string key);
        void AddDisplayedLevelNumber();
        int GetDisplayedLevelNumber();
        void AddSession();
        int GetSessionCount();
        DateTime GetRegistrationDate();
        void SetLastLoginDate();
        DateTime GetLastLoginDate();
        int GetNumberDaysAfterRegistration();
        void SetSoft(int value);
        int GetSoft();
        DateTime GetSaveTime();
        void SetLevel(int index);
        int GetLevel();
        void Save();
        void ClearData();
#if UNITY_WEBGL
        void SaveRemote();
        IEnumerator SyncRemoteSave(Action onDataIsSynchronizedCallback = null);
        IEnumerator ClearDataRemote(Action onRemoteDataCleared = null);
#endif
        event Action Changed;
    }
}