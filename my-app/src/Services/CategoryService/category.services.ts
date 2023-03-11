import axios, { AxiosResponse } from "axios";
import { Category } from "../../Models/Category";

export const GetListOfNewsCategories = (): Promise<Category[]> => {
  return axios
    .get<Category[]>("http://localhost:5014/Category")
    .then((response: AxiosResponse<Category[]>) => {
      const categories: Category[] = response.data;
      console.log(categories);
      return categories;
    })
    .catch((error) => {
      const errorMessage = error.response?.data?.message ?? "An error occurred";
      alert(errorMessage);
      throw new Error(errorMessage);
    });
};
