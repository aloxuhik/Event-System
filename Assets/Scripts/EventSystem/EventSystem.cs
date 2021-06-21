using System;
using System.Collections.Generic;
using UnityEngine;

namespace ES
{
    public class EventSystem : MonoBehaviour
    {
        // Use this for initialization
        void OnEnable()
        {
            if(__Current == null)
            __Current = this;
            else if(__Current != this)
            {
                //Ensure there is only one EventSystem
                Destroy(gameObject);
            }
        }

        static private EventSystem __Current;
        static public EventSystem Current
        {
            get
            {
                if (__Current == null)
                {
                    __Current = FindObjectOfType<EventSystem>();
                }                

                return __Current;
            }
        }
        public delegate void EListener<T>(T arg);
        public delegate bool ConditionDelegate<T>(T arg);

        Dictionary<Type, List<eListenerInfo>> eListeners;

        /// <summary>
        /// Register this listener as first one to receive event. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listener"></param>
        public void RegisterListenerLead<T>(EListener<T> listener) where T : EventInfo
        {
            Type eventType = typeof(T);
            var newListener = CreateListenerInfo(listener);

            eListeners[eventType].Insert(0, newListener);
        }

        /// <summary>
        /// Unregister Listener.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listener"></param>
        public void UnregisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            Type eventType = typeof(T);
            for(int i = 0; i < eListeners[eventType].Count; i++)
            {
                if (listener.Equals(eListeners[eventType][i].FunctionCallback))
                {
                    eListeners[eventType].RemoveAt(i);
                    i++;
                }
            }
        }

        /// <summary>
        /// Wrapping listener and condition together.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listener"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private eListenerInfo CreateListenerInfo<T>(EListener<T> listener, Func<T, bool> predicate = null) where T : EventInfo
        {
            Type eventType = typeof(T);
            if (eListeners == null)
            {
                eListeners = new Dictionary<Type, List<eListenerInfo>>();
            }

            if (eListeners.ContainsKey(eventType) == false || eListeners[eventType] == null)
            {
                eListeners[eventType] = new List<eListenerInfo>();
            }
            eListenerInfo newListner = new eListenerInfo();
            newListner.FunctionCallback = listener;
            newListner.CallbackCondition = predicate;

            return (newListner);
        }

        /// <summary>
        /// Register listener for passed EventInfo Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listener"></param>
        public void RegisterListener<T>(EListener<T> listener) where T : EventInfo
        {
            Type eventType = typeof(T);
            var newListener = CreateListenerInfo(listener);
            eListeners[eventType].Add(newListener);
        }
        
        /// <summary>
        /// Register callback for passed EvenInfo Type with condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listener">Callback function</param>
        /// <param name="predicate">Condition for callback</param>
        public void RegisterListener<T>(EListener<T> listener, Func<T, bool> predicate) where T : EventInfo
        {
            if(predicate == null)
            {
                Debug.Log("ASA");
            }
            Type eventType = typeof(T);
            var newListener = CreateListenerInfo(listener,predicate);
            eListeners[eventType].Add(newListener);
        }

        /// <summary>
        /// Fire Event, Call all listeners registered with same type as <paramref name="eventInfo"/>
        /// </summary>
        /// <param name="eventInfo"></param>
        public void FireEvent(EventInfo eventInfo)
        {
            Type trueEventInfoClass = eventInfo.GetType();
            if (eListeners == null || !eListeners.ContainsKey(trueEventInfoClass))
            {
                // No one is listening, we are done.
                return;
            }
            foreach (var el in eListeners[trueEventInfoClass])
            {
                //Check if Callback have condition for being called
                if (el.CallbackCondition != null) {
                    var ret = (bool)el.CallbackCondition.DynamicInvoke(eventInfo);
                    if (ret == false)
                        continue;
                }

                el.FunctionCallback.DynamicInvoke(eventInfo);
            }
        }

        /// <summary>
        /// Wrapper to unity Callback function with Callback condition
        /// </summary>
    private class eListenerInfo
        {
            public Delegate FunctionCallback;
            public Delegate CallbackCondition;
        }
    }
}