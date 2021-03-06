﻿using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using RAMACHAT.Model;
using RAMACHAT.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace RAMACHAT.SocketIO
{
    public class Connection
    {
        public string TOKEN = null;
        public string HOSTNAME = "http://128.199.113.218:3000";
        public Socket mSocket;

        public Socket getSocket()
        {
            return mSocket;
        }
        public void addItems()
        {

        }
        public void connectSocket()
        {
            if (this.TOKEN != null)
                {
                    try
                    {
                        Dictionary<string, string> _author = new Dictionary<string, string>();
                        _author.Add("token", this.TOKEN);
                        IO.Options _option = new IO.Options();
                        //_option.Timeout = 5000;
                        _option.Query = _author;
                        _option.ForceNew = true;
                        _option.Reconnection = true;
                        _option.ReconnectionDelay = 500;
                        
                        mSocket = IO.Socket(HOSTNAME, _option);
                        mSocket.On(Socket.EVENT_CONNECT, () =>
                        {
                            Dispatcher dispatcher = Deployment.Current.Dispatcher;
                            dispatcher.BeginInvoke(() =>
                            {
                              //  MessageBox.Show("OK");
                            });
                            Debug.WriteLine("OK");
                        });
                        mSocket.On(Socket.EVENT_CONNECT_ERROR, onConnectError);
                        mSocket.On(Socket.EVENT_CONNECT_TIMEOUT, onConnectTimeout);
                        mSocket.On(Socket.EVENT_ERROR, () =>
                        {
                            Dispatcher dispatcher = Deployment.Current.Dispatcher;
                            dispatcher.BeginInvoke(() =>
                            {
                               // MessageBox.Show("ERROR");
                                mSocket.Connect();
                                mSocket.Open();
                            });
                        });
                        mSocket.On(Constant.SOCKET_EVENT_JOIN, onJoinRoom);
                        mSocket.On(Constant.SOCKET_EVENT_ADD, onAddUser);
                        mSocket.On(Constant.SOCKET_EVENT_LEAVE, onLeaveRoom);
                        mSocket.On(Constant.SOCKET_EVENT_CHANGE_ROOM_TITLE, onChangeRoomTitle);
                        mSocket.On(Constant.SOCKET_EVENT_CHAT, (data) => {
                            Debug.WriteLine(data.ToString());
                            Dispatcher dispatcher = Deployment.Current.Dispatcher;
                            dispatcher.BeginInvoke(() =>
                            {
                                String result = data.ToString();
                                ChatResponse resultObject = JsonConvert.DeserializeObject<ChatResponse>(result);
                                if(resultObject.data.message.type == 2)
                                {
                                    App.ViewModel.Items.Add(new ViewModels.ItemViewModel() { SenderID = resultObject.data.sender._id, CreateAt = resultObject.data.sequence, MessageText = resultObject.data.message.message, Avatar = new Uri(resultObject.data.sender.avatar), Type = resultObject.data.message.type, thumbnail = new Uri(resultObject.data.message.file.thumbnail, UriKind.RelativeOrAbsolute) });
                                }
                                else
                                {
                                    App.ViewModel.Items.Add(new ViewModels.ItemViewModel() { SenderID = resultObject.data.sender._id, CreateAt = resultObject.data.sequence, MessageText = resultObject.data.message.message, Avatar = new Uri(resultObject.data.sender.avatar), Type = resultObject.data.message.type });
                                }
                                
                                if(resultObject.data.sender._id != App._userid)
                                {
                                    var stream = Application.GetResourceStream(new Uri(@"Assets/Audio/recieve.wav", UriKind.RelativeOrAbsolute));
                                    var effect = SoundEffect.FromStream(stream.Stream);
                                    var soundInstance = effect.CreateInstance();

                                    FrameworkDispatcher.Update();
                                    soundInstance.Play();
                                    App._reuserid = resultObject.data.sender._id;
                                    ToastPrompt tost = new ToastPrompt()
                                    {
                                        Title = resultObject.data.sender.username,
                                        Message = resultObject.data.message.message,
                                    };
                                    tost.Tap += tosk_Tap;
                                    tost.Show();
                                }
                            });
                        });
                        mSocket.Connect();
                    }
                catch(Exception e)
                    {
                        Debug.WriteLine("IO exception  " + e.Message);
                        //mSocket.Close();
                       // mSocket.Open();
                    }
                }
                else
                {
                    Debug.WriteLine("No token found!");
                }
        }

        private void tosk_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ChatPage.xaml", UriKind.RelativeOrAbsolute));
        }
        public void sendMesS(string senderId, bool isGroup, string message, int type, string senderName, JArray memberArray, string sequence)
        {
            JObject messageObject = new JObject();
            messageObject.Add("senderId", senderId);
            messageObject.Add("isGroup", isGroup);
            messageObject.Add("message", message);
            messageObject.Add("type", type);
            messageObject.Add("senderName", senderName);
            messageObject.Add("members", memberArray);
            messageObject.Add("sequence", sequence);

            mSocket.Emit("chat", messageObject);
        }
        public void disconnectSocket()
        {
            if (mSocket != null)
            {
                mSocket.Disconnect();
                mSocket.Off();
            }
        }
        
        
        private void onChangeRoomTitle(object obj)
        {
            Debug.WriteLine("onChangeRoomTitle");
        }

        private void onLeaveRoom(object obj)
        {
            Debug.WriteLine("onLeaveRoom");
        }

        private void onAddUser(object obj)
        {
            Debug.WriteLine("onAddUser");
        }

        private void onJoinRoom(object obj)
        {
            Debug.WriteLine("onJoinRoom");
        }

        private void onConnect(object obj)
        {
            Debug.WriteLine("Connect success!");
        }

        private void onConnectTimeout(object obj)
        {
            Dispatcher dispatcher = Deployment.Current.Dispatcher;
            dispatcher.BeginInvoke(() =>
            {
                //MessageBox.Show("ERROR TIME OUT");
                mSocket.Connect();
            });
        }

        private void onConnectError(object obj)
        {
            Dispatcher dispatcher = Deployment.Current.Dispatcher;
            dispatcher.BeginInvoke(() =>
            {
                //MessageBox.Show("ERROR Connect ");
                mSocket.Connect();
            });
        }

    }
    
}
