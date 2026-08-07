export const saleKeys = {
  all: ["sales"] as const,

  list: () =>
    [...saleKeys.all, "list"] as const,
};