using Moq;
using Xunit;
using ServiceLayer.Services;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Dtos;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;

public class OrderServiceTests
{
	private readonly Mock<IOrderItemService> _orderItemServiceMock;
	private readonly Mock<IEmailService> _emailServiceMock;
	private readonly Mock<IOrderRepository> _orderRepositoryMock;
	private readonly Mock<IProductService> _productServiceMock;
	private readonly Mock<IinvoiceService> _invoiceServiceMock;
	private readonly Mock<ICustomerService> _customerServiceMock;
	private readonly OrderService _orderService;

	public OrderServiceTests()
	{
		_orderItemServiceMock = new Mock<IOrderItemService>();
		_emailServiceMock = new Mock<IEmailService>();
		_orderRepositoryMock = new Mock<IOrderRepository>();
		_productServiceMock = new Mock<IProductService>();
		_invoiceServiceMock = new Mock<IinvoiceService>();
		_customerServiceMock = new Mock<ICustomerService>();

		_orderService = new OrderService(
			_orderItemServiceMock.Object,
			_emailServiceMock.Object,
			_orderRepositoryMock.Object,
			_productServiceMock.Object,
			_invoiceServiceMock.Object,
			_customerServiceMock.Object
		);
	}

	[Fact]
	public async Task AddOrder_ShouldReturnMessages_WhenProductIsNotExist()
	{
		// Arrange
		var order = new Order
		{
			OrderItems = new List<OrderItem>
			{
				new OrderItem { ProductId = 1, Quantity = 5 }
			}
		};

		_productServiceMock.Setup(service => service.ProductById(It.IsAny<int>())).ReturnsAsync((Product)null);

		// Act
		var result = await _orderService.AddOrder(order);

		// Assert
		result.Messages.Should().Contain("Product with Id : 1 is not exist");
	}

	//[Fact]
	//public async Task AddOrder_ShouldReturnMessages_WhenStockIsInsufficient()
	//{
	//	// Arrange
	//	var order = new Order
	//	{
	//		OrderItems = new List<OrderItem>
	//		{
	//			new OrderItem { ProductId = 1, Quantity = 5 }
	//		}
	//	};

	//	_productServiceMock.Setup(service => service.ProductById(It.IsAny<int>())).ReturnsAsync(new Product { Id = 1, Stock = 3 });

	//	// Act
	//	var result = await _orderService.AddOrder(order);

	//	// Assert
	//	result.Messages.Should().Contain($"Product : 1 is not sufficient for the requested quantity.");
	//}

	[Fact]
	public async Task AddOrder_ShouldApplyDiscount_WhenTotalAmountExceedsThreshold()
	{
		// Arrange
		var order = new Order
		{
			OrderItems = new List<OrderItem>
			{
				new OrderItem { ProductId = 1, Quantity = 2 }
			}
		};

		var product = new Product { Id = 1, Stock = 10, Price = 150 };
		_productServiceMock.Setup(service => service.ProductById(It.IsAny<int>())).ReturnsAsync(product);
		_orderRepositoryMock.Setup(repo => repo.AddOrder(It.IsAny<Order>())).ReturnsAsync(order);

		// Act
		var result = await _orderService.AddOrder(order);

		// Assert
		result.Order.TotalAmount.Should().Be(270); // 10% discount applied
	}

	[Fact]
	public async Task AddOrder_ShouldUpdateProductStock_WhenOrderIsPlaced()
	{
		// Arrange
		var order = new Order
		{
			OrderItems = new List<OrderItem>
			{
				new OrderItem { ProductId = 1, Quantity = 2 }
			}
		};

		var product = new Product { Id = 1, Stock = 10, Price = 100 };
		_productServiceMock.Setup(service => service.ProductById(It.IsAny<int>())).ReturnsAsync(product);
		_orderRepositoryMock.Setup(repo => repo.AddOrder(It.IsAny<Order>())).ReturnsAsync(order);

		// Act
		var result = await _orderService.AddOrder(order);

		// Assert
		_productServiceMock.Verify(service => service.UpdateProduct(It.Is<Product>(p => p.Stock == 8)), Times.Once);
	}

	[Fact]
	public async Task AddOrder_ShouldGenerateInvoice_WhenOrderIsPlaced()
	{
		// Arrange
		var order = new Order
		{
			OrderItems = new List<OrderItem>
			{
				new OrderItem { ProductId = 1, Quantity = 2 }
			},
			TotalAmount = 200,
			Name = "Test Order"
		};

		var product = new Product { Id = 1, Stock = 10, Price = 100 };
		_productServiceMock.Setup(service => service.ProductById(It.IsAny<int>())).ReturnsAsync(product);
		_orderRepositoryMock.Setup(repo => repo.AddOrder(It.IsAny<Order>())).ReturnsAsync(order);

		// Act
		var result = await _orderService.AddOrder(order);

		// Assert
		_invoiceServiceMock.Verify(service => service.AddInvoice(It.IsAny<Invoice>()), Times.Once);
	}

	[Fact]
	public async Task ApplyDiscount_ShouldApply5Percent_WhenTotalAmountIsGreaterThan100()
	{
		// Arrange
		var order = new Order
		{
			TotalAmount = 150
		};

		// Act
		_orderService.ApplyDiscount(order);

		// Assert
		order.TotalAmount.Should().Be(142.5m); // 5% discount
	}

	[Fact]
	public async Task ApplyDiscount_ShouldApply10Percent_WhenTotalAmountIsGreaterThan200()
	{
		// Arrange
		var order = new Order
		{
			TotalAmount = 250
		};

		// Act
		_orderService.ApplyDiscount(order);

		// Assert
		order.TotalAmount.Should().Be(225m); // 10% discount
	}
}
