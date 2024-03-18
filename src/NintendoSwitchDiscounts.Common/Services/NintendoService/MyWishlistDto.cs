// ReSharper disable ClassNeverInstantiated.Global

namespace NintendoSwitchDiscounts.Common.Services.NintendoService;

public record MyWishlistDto(bool HasNextPage, IEnumerable<ItemDto> Items);

public record ItemDto(ProductDto Product);

public record ProductDto(string Id);