// ReSharper disable ClassNeverInstantiated.Global

namespace NintendoSwitchDiscounts.Common.Services.NintendoService;

public record ContentsDto(IEnumerable<ContentDto> Contents);

public record ContentDto(long Id, string FormalName);