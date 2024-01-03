// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/orders.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace OrderGrpc {
  /// <summary>
  /// The greeting service definition.
  /// </summary>
  public static partial class Order
  {
    static readonly string __ServiceName = "order.Order";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::OrderGrpc.OrderRequest> __Marshaller_order_OrderRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::OrderGrpc.OrderRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::OrderGrpc.OrderResponse> __Marshaller_order_OrderResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::OrderGrpc.OrderResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::OrderGrpc.OrderRequest, global::OrderGrpc.OrderResponse> __Method_PlaceOrder = new grpc::Method<global::OrderGrpc.OrderRequest, global::OrderGrpc.OrderResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "PlaceOrder",
        __Marshaller_order_OrderRequest,
        __Marshaller_order_OrderResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::OrderGrpc.OrderRequest, global::OrderGrpc.OrderResponse> __Method_UpdateOrder = new grpc::Method<global::OrderGrpc.OrderRequest, global::OrderGrpc.OrderResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateOrder",
        __Marshaller_order_OrderRequest,
        __Marshaller_order_OrderResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::OrderGrpc.OrdersReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Order</summary>
    [grpc::BindServiceMethod(typeof(Order), "BindService")]
    public abstract partial class OrderBase
    {
      /// <summary>
      /// place a new order
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::OrderGrpc.OrderResponse> PlaceOrder(global::OrderGrpc.OrderRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Update exiting order
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::OrderGrpc.OrderResponse> UpdateOrder(global::OrderGrpc.OrderRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(OrderBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_PlaceOrder, serviceImpl.PlaceOrder)
          .AddMethod(__Method_UpdateOrder, serviceImpl.UpdateOrder).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, OrderBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_PlaceOrder, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::OrderGrpc.OrderRequest, global::OrderGrpc.OrderResponse>(serviceImpl.PlaceOrder));
      serviceBinder.AddMethod(__Method_UpdateOrder, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::OrderGrpc.OrderRequest, global::OrderGrpc.OrderResponse>(serviceImpl.UpdateOrder));
    }

  }
}
#endregion
