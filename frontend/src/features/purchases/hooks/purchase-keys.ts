export const purchaseKeys = {
  all: ["purchases"] as const,
  list: () => [...purchaseKeys.all, "list"] as const,
};