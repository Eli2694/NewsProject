import axios, { AxiosResponse } from "axios";
import { Category } from "../../Models/Category";
import { Users } from "../../Models/Users";

export const LoginUser = async (email: string | undefined) => {
  if (typeof email !== "string") {
    console.log("User email is not a valid string");
    return;
  }
  try {
    const response = await axios.post(`http://localhost:5014/User/${email}`, {
      Email: email,
    });
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

export const UpdateUserCategoriesInDB = async (user: Users | undefined) => {
  if (user === undefined) {
    console.log("User did not choose categories");
    return;
  }
  try {
    const response = await axios.put(`http://localhost:5014/User`, user);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error("An error occurred ");
  }
};

export const getUserCategories = async (
  email: string | undefined
): Promise<Category[]> => {
  try {
    const response: AxiosResponse<Category[]> = await axios.get(
      `http://localhost:5014/User/${email}`
    );
    return response.data;
  } catch (error) {
    console.log(error);
    throw new Error("Failed to get user categories.");
  }
};
