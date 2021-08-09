using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImpactWorks.FBGraph.Connector;
using ImpactWorks.FBGraph.Core;

namespace Silkways
{
    public class Authentication
    {
        public ImpactWorks.FBGraph.Connector.Facebook FacebookAuth()
        {
            //Setting up the facebook object
            ImpactWorks.FBGraph.Connector.Facebook facebook = new ImpactWorks.FBGraph.Connector.Facebook();
            facebook.AppID = "266466697147513";
            facebook.CallBackURL = "http://localhost:17959/Test/Success/";
            facebook.Secret = "6ca56e06c2409e8b273aba5be02e1a98";

            //Setting up the permissions
            List<FBPermissions> permissions = new List<FBPermissions>() {
                FBPermissions.user_about_me, // to read about me               
                FBPermissions.user_events,
                FBPermissions.user_status,
                FBPermissions.read_stream,
                FBPermissions.friends_events,
                FBPermissions.publish_stream
            };

            //Pass the permissions object to facebook instance
            facebook.Permissions = permissions;
            return facebook;
        }
    }
}