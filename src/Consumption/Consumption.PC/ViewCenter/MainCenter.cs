﻿/*
*
* 文件名    ：MainCenter                             
* 程序说明  : 首页控制类 
* 更新时间  : 2020-05-21 17：31
* 联系作者  : QQ:779149549 
* 开发者群  : QQ群:874752819
* 邮件联系  : zhouhaogg789@outlook.com
* 视频教程  : https://space.bilibili.com/32497462
* 博客地址  : https://www.cnblogs.com/zh7791/
* 项目地址  : https://github.com/HenJigg/WPF-Xamarin-Blazor-Examples
* 项目说明  : 以上所有代码均属开源免费使用,禁止个人行为出售本项目源代码
*/

namespace Consumption.PC.ViewCenter
{
    using Consumption.Core.Common;
    using Consumption.Core.Interfaces;
    using Consumption.PC.View;
    using Consumption.ViewModel;
    using GalaSoft.MvvmLight.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// 首页控制类
    /// </summary>
    public class MainCenter : BaseDialogCenter<MainWindow, MainViewModel>
    {
        public override void RegisterMessenger()
        {
            Messenger.Default.Register<string>(GetDialog(), "NavigationNewPage", arg =>
            {
                var module = AutofacProvider.Get<IModule>(arg);
                if (module != null)
                {
                    module.BindDefaultModel();
                    View.page.Content = module.GetView();
                }
                else
                {
                    //...
                }
            });
            Messenger.Default.Register<string>(GetDialog(), "UpdateBackground", arg =>
            {
                ViewModel.StyleConfig.Url = arg;
                //保存用户配置...
            });
            Messenger.Default.Register<double>(GetDialog(), "UpdateTrans", arg =>
            {
                ViewModel.StyleConfig.Trans = arg / 100;
                //保存用户配置...
            });
            Messenger.Default.Register<double>(GetDialog(), "UpdateGaussian", arg =>
            {
                ViewModel.StyleConfig.Radius = arg;
                //保存用户配置...
            });
        }

        public override async void BindDefaultViewModel()
        {
            if (ViewModel == null)
            {
                ViewModel = new MainViewModel();
                await ViewModel.InitDefaultView();
                View.page.Content = new HomeView();
                View.DataContext = ViewModel;
            }
        }
    }
}
