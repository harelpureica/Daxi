using Firebase.Crashlytics;
using Firebase;
using UnityEngine;

public class CrashlyticsInit : MonoBehaviour
{
    [SerializeField]
    private bool initTestCrash;
         
    int updatesBeforeException;

    void Start()
    {
        FirebaseApp.LogLevel = Firebase.LogLevel.Debug;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
           
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // Crashlytics will use the DefaultInstance, as well;
                // this ensures that Crashlytics is initialized.
                FirebaseApp app = FirebaseApp.DefaultInstance;

                // When this property is set to true, Crashlytics will report all
                // uncaught exceptions as fatal events. This is the recommended behavior.
                Crashlytics.ReportUncaughtExceptionsAsFatal = true;

                // Set a flag here for indicating that your project is ready to use Firebase.
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        updatesBeforeException = 0;
    }


    void Update()
    {
        // Call the exception-throwing method here so that it's run
        // every frame update
        if(initTestCrash)
        {
            throwExceptionEvery60Updates();
        }
    }

    // A method that tests your Crashlytics implementation by throwing an
    // exception every 60 frame updates. You should see reports in the
    // Firebase console a few minutes after running your app with this method.
    void throwExceptionEvery60Updates()
    {
        if (updatesBeforeException > 0)
        {
            updatesBeforeException--;
        }
        else
        {
            // Set the counter to 60 updates
            updatesBeforeException = 60;

            // Throw an exception to test your Crashlytics implementation
            throw new System.Exception("test exception please ignore");
        }

    }
}