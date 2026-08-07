export const kardexKeys = {
  all: ["kardex"] as const,

  products: () =>
    [...kardexKeys.all, "products"] as const,

  movements: (productId: number) =>
    [...kardexKeys.all, "movements", productId] as const,
};