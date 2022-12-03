using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

public class ServerConnection
{
    private Func<UnityWebRequest> requestSetter = null;
    public Action<string> serverInteraction = null;
    private long statusCode;
    public bool isFail = false;
    public bool isNetworkError = false;
    

    public IEnumerator ConnectRoutine()
    {
        var request = requestSetter();
        yield return ConnectToServer(request);

        if(IsConnectFail(statusCode))
        {
            for(int i=0; i<2; i++)
            {
                yield return RetryConnectToServer();

                if(IsConnectFail(statusCode) == false)
                {
                    break;
                }
                else if(i == 1 && isNetworkError == true)
                {
                    Debug.Log("Fail 3 times");
                    // if (NetworkPopup.isPopupExist != true) ShowNetworkPopup();
                }
            }
        }

        serverInteraction = null;
        requestSetter = null;
    }
    

    private IEnumerator ConnectToServer(UnityWebRequest www)
    {
        using(www)
        {
            yield return www.SendWebRequest();
            statusCode = www.responseCode;
            Debug.Log("ResponseCode :" + www.responseCode);

            if(www.isDone)
            {
                Debug.Log($"status code : {statusCode}");
                switch(statusCode)
                {
                    case 200 :
                    case 201 :
                        string json = www.downloadHandler.text;
                        Debug.Log(json);
                        if(serverInteraction != null)
                        {
                            serverInteraction(json);
                        }
                        
                        break;
                    default :
                        Debug.Log("Server 연결 실패");
                        break;
                }
            }
            else
            {
                Debug.Log("Server 연결 실패");
            }
        }
    }


    private bool IsConnectFail(long statusCode)
    {
        switch(statusCode)
        {
            case 200 :
            case 201 :
                isFail = false;
                isNetworkError = false;
                return false;
            case 401 :
            case 403 :
            case 404 :
                isFail = true;
                isNetworkError = false;
                return true;
            default :
                isFail = true;
                isNetworkError = true;
                return true;
        }
    }

    private IEnumerator RetryConnectToServer()
    {
        UnityWebRequest request = null;

        switch(statusCode)
        {
            case 200 :
            case 201 :
                break;
            case 401 :
                // yield return TokenRefresh();
                if(statusCode == 201)
                {
                    Debug.Log("After RefreshToken");
                    request = requestSetter();
                    yield return ConnectToServer(request);
                }
                break;
            case 404 :
            default :
                request = requestSetter();
                yield return ConnectToServer(request);
                break;
        }
    }
}

