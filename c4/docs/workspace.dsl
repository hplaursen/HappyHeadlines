workspace {
    model {
        Publisher = person "Publisher" "reates, reviews and publishes articles"
        Reader = person "Reader" "Reads articles, posts comments and subscribes to the newsletter"      
        Datadog = softwareSystem "Datadog" "Monitoring platform for metrics, logs and traces"

        HappyHeadlinesSystem = softwareSystem "Happy Headlines System" "A website and newsletter that distribute happy news arround the world" {
            WebApp = container "Web Application" "C#.NET" "Application used by publishers to create, review and publish articles"
            WebSite = container "Web Site" "C#.NET" "Website used by readers to read articles, comment and subscribe"

            DraftService = container "DraftService" "C#.NET Web API" "Provides a list of supported languages." {
            } 
            DraftDatabase = container "DraftDatabase" "C#.NET Web API" "Provides a list of supported languages." {
            } 
            publisherService = container "publisherService" "C#.NET Web API" "Provides a list of supported languages." {
            }
            ArticleQueue = container "ArticleQueue" "C#.NET Web API" "Provides a list of supported languages." {
            }
            ArticleDatabase = container "ArticleDatabase" "C#.NET Web API" "Provides a list of supported languages." {
            }


            ArticleService = container "ArticleService" "C#.NET Web API" "Provides a list of supported languages." {
            }
            
            CommentService = container "CommentService" "C#.NET Web API" "Provides a random greeting based on the language." {
            }

            CommentDatabase = container "CommentDatabase" "C#.NET Web API" "Provides a random greeting based on the language." {
            }

            SubscriberService = container "SubscriberService" "C#.NET Web API" "Provides a random greeting based on the language." {
            }

            SubscriberDatabase = container "SubscriberDatabase" "C#.NET Web API" "Provides a random greeting based on the language." {
            }

            SubscriberQueue = container "SubscriberQueue" "C#.NET Web API" "Provides a random greeting based on the language." {
            }


            NewsletterService = container "NewsletterService" "C#.NET Web API" "Distribute happy news arround the world"
            ProfanityService = container "ProfanityService" "C#.NET Web API" "Provides a random greeting based on the language." {
            }
            ProfanityDatabase = container "ProfanityDatabase" "C#.NET Web API" "Provides a random greeting based on the language." {
            }

            MonitoringAgent = container "Monitoring Agent" "Datadog Agent" "Collects metrics, logs and traces from the application"

        }
        
        // Relationships
        Publisher -> WebApp "Interacts with"
        Reader -> WebSite "Interacts with"
        MonitoringAgent -> Datadog "send metrics, logs and traces"
        
        WebApp -> MonitoringAgent "Uses"
        publisherService -> MonitoringAgent "Uses"
        ArticleService -> MonitoringAgent "Uses"
        CommentService -> MonitoringAgent "Uses" 
        SubscriberService -> MonitoringAgent "Uses" 

        WebApp -> DraftService "Uses"
        DraftService -> DraftDatabase "Uses"
        WebApp -> publisherService "Uses"
        publisherService -> ProFanityService "Uses" 
        publisherService -> ArticleQueue "Uses" 
        ArticleQueue -> ArticleDatabase "Uses" 

        WebSite -> ArticleService "Uses" 
        ArticleService -> ArticleDatabase "Uses" 
        WebSite -> CommentService "Uses" 
        CommentService -> ProfanityService "Uses" 
        CommentService -> CommentDatabase "Uses" 
        WebSite -> SubscriberService "Uses" 
        SubscriberService -> SubscriberDatabase "Uses" 
        SubscriberService -> SubscriberQueue "Uses" 

       ProFanityService -> ProfanityDatabase "Uses" 



    }

    views {
        styles {
            element "Element" {
                color #ffffff
            }
            element "Person" {
                background #9b191f
                shape person
            }
            element "Software System" {
                background #ba1e25
            }
            element "App" {
                shape "MobileDeviceLandscape"
            }
            element "Database" {
                shape cylinder
            }
            element "Container" {
                background #d9232b
            }
            element "Component" {
                background #E66C5A
            }
            element "WebBrowser" {
                shape WebBrowser
            }
        }
    }
}