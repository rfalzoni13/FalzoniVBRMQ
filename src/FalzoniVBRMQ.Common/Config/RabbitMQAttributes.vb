Namespace Config
    Public NotInheritable Class RabbitMQAttributes
        ' Exchanges
        Shared Property EXG_DIRECT_NAME As String = "falzoniexg.direct"
        Shared Property EXG_FANOUT_NAME As String = "falzoniexg.fanout"
        Shared Property EXG_TOPIC_NAME As String = "falzoniexg.topic"

        ' Product
        Shared Property QUEUE_PRODUCT_DATA As String = "product.data"
        Shared Property RK_PRODUCT_DATA As String = "product.data"

        Shared Property QUEUE_PRODUCT_LOG As String = "product.log"
        Shared Property RK_PRODUCT_LOG As String = "product.log"

        Shared Property RK_PRODUCT_ALL As String = "product.*"
    End Class
End Namespace
