Imports System.Linq.Expressions
Imports System.Runtime.Remoting.Messaging
Imports FalzoniVBRMQ.Common.Config
Imports FalzoniVBRMQ.Consumer.Workers
Imports FalzoniVBRMQ.Tests.Utils
Imports Moq

<TestClass()> Public Class ConsumerTest
    Private Property _list As List(Of String)
    Private Property _mock As Mock(Of ConsumerWorker)
    Private Property _predicateSingle As Expression(Of Func(Of ConsumerWorker, List(Of String)))
    Private Property _predicateAll As Expression(Of Func(Of ConsumerWorker, List(Of String)))
    'Private Property _predicateAllAsync As Expression(Of Func(Of ConsumerWorker, Task(Of List(Of String))))

    <TestInitialize()> Public Sub TestInitialize()
        _mock = New Mock(Of ConsumerWorker)()

        _predicateSingle = Function(m) m.Consume(It.IsAny(Of String)())
        _predicateAll = Function(m) m.ConsumeAll(It.IsAny(Of String)())
        '_predicateAllAsync = Function(m) m.ConsumeAllAsync(It.IsAny(Of String)())
    End Sub

    <TestMethod()> Public Sub TestRunConsumerAll_Success()
        _list = New List(Of String) From
        {
            "Teste de mensagem Direct",
            "Teste de mensagem Fanout",
            "Teste de mensagem Topic"
        }

        Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMock(_mock, _predicateAll, _list)

        Dim messages = obj.ConsumeAll(RabbitMQAttributes.QUEUE_PRODUCT_DATA)

        Assert.AreEqual(messages, _list)
    End Sub

    <TestMethod()> Public Sub TestRunConsumerAllAsync_Success()
        _list = New List(Of String) From
        {
            "Teste de mensagem Direct",
            "Teste de mensagem Fanout",
            "Teste de mensagem Topic"
        }

        'Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMockAsync(_mock, _predicateAllAsync, _list)

        _mock.Setup(Function(m) m.ConsumeAllAsync(It.IsAny(Of String)())).Returns(Task.FromResult(_list))

        Dim messages = _mock.Object.ConsumeAllAsync(RabbitMQAttributes.QUEUE_PRODUCT_DATA).Result

        Assert.AreEqual(messages, _list)
    End Sub

    <TestMethod()> Public Sub TestRunConsumerAll_Error()
        _list = New List(Of String) From
        {
            "Teste de mensagem Direct",
            "Teste de mensagem Fanout",
            "Teste de mensagem Topic"
        }

        Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMock(_mock, _predicateAll, _list)

        Dim messages = obj.ConsumeAll(RabbitMQAttributes.QUEUE_PRODUCT_DATA)

        Assert.AreNotEqual(messages, New List(Of String) From
        {
            "Teste de mensagem Direct",
            "Teste de mensagem Fanout",
            "Teste de mensagem Topic"
        })
    End Sub

    <TestMethod()> Public Sub TestRunConsumerAllAsync_Error()
        _list = New List(Of String) From
        {
            "Teste de mensagem Direct",
            "Teste de mensagem Fanout",
            "Teste de mensagem Topic"
        }

        'Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMockAsync(_mock, _predicateAllAsync, _list)

        _mock.Setup(Function(m) m.ConsumeAllAsync(It.IsAny(Of String)())).Returns(Task.FromResult(_list))

        Dim messages = _mock.Object.ConsumeAllAsync(RabbitMQAttributes.QUEUE_PRODUCT_DATA).Result

        Assert.AreNotEqual(messages, New List(Of String) From
        {
            "Teste de mensagem Direct",
            "Teste de mensagem Fanout",
            "Teste de mensagem Topic"
        })
    End Sub

    <ExpectedException(GetType(AssertFailedException))>
    <TestMethod()> Public Sub TestRunConsumerAll_Fail()
        Try
            Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupExceptionReturnMock(_mock, _predicateAll)
            obj.ConsumeAll(RabbitMQAttributes.QUEUE_PRODUCT_LOG)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub

    <ExpectedException(GetType(AssertFailedException))>
    <TestMethod()> Public Sub TestRunConsumerAllAsync_Fail()
        Try
            'Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupExceptionReturnMockAsync(_mock, _predicateAllAsync)

            _mock.Setup(Function(m) m.ConsumeAllAsync(It.IsAny(Of String)())).Throws(New Exception)

            _mock.Object.ConsumeAllAsync(RabbitMQAttributes.QUEUE_PRODUCT_LOG).RunSynchronously()
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub

    <TestMethod()> Public Sub TestRunConsumerSingle_Direct_Success()
        _list = New List(Of String) From
        {
            "Teste de mensagem Direct"
        }

        Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMock(_mock, _predicateSingle, _list)

        Dim messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_DATA)

        Assert.AreEqual(messages, _list)
    End Sub

    <TestMethod()> Public Sub TestRunConsumerSingle_Fanout_Success()
        _list = New List(Of String) From
        {
            "Teste de mensagem Fanout"
        }

        Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMock(_mock, _predicateSingle, _list)

        Dim messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_DATA)

        Assert.AreEqual(messages, _list)
    End Sub

    <TestMethod()> Public Sub TestRunConsumerSingle_Topic_Success()
        _list = New List(Of String) From
        {
            "Teste de mensagem Topic"
        }

        Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMock(_mock, _predicateSingle, _list)

        Dim messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG)

        Assert.AreEqual(messages, _list)
    End Sub

    <TestMethod()> Public Sub TestRunConsumerSingle_Direct_Error()
        _list = New List(Of String) From
        {
            "Teste de mensagem Direct"
        }

        Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMock(_mock, _predicateSingle, _list)

        Dim messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG)

        Assert.AreNotEqual(messages, New List(Of String) From
        {
            "Teste de mensagem Direct"
        })
    End Sub

    <TestMethod()> Public Sub TestRunConsumerSingle_Fanout_Error()
        _list = New List(Of String) From
        {
            "Teste de mensagem Fanout"
        }

        Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMock(_mock, _predicateSingle, _list)

        Dim messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG)

        Assert.AreNotEqual(messages, New List(Of String) From
        {
            "Teste de mensagem Fanout"
        })
    End Sub

    <TestMethod()> Public Sub TestRunConsumerSingle_Topic_Error()
        _list = New List(Of String) From
        {
            "Teste de mensagem Topic"
        }

        Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupReturnMock(_mock, _predicateSingle, _list)

        Dim messages = obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG)

        Assert.AreNotEqual(messages, New List(Of String) From
        {
            "Teste de mensagem Topic"
        })
    End Sub

    <ExpectedException(GetType(AssertFailedException))>
    <TestMethod()> Public Sub TestRunProducerSingle_Fail()
        Try
            Dim obj As ConsumerWorker = TestUtils(Of ConsumerWorker, List(Of String)).SetupExceptionReturnMock(_mock, _predicateSingle)
            obj.Consume(RabbitMQAttributes.QUEUE_PRODUCT_LOG)
        Catch ex As Exception
            Assert.Fail()
        End Try
    End Sub
End Class