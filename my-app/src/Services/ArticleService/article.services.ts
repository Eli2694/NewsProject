import axios from "axios";
import { Article } from "../../Models/Article";
import { Users } from "../../Models/Users";

// export const fetchArticles = async (user: Users): Promise<Article[]> => {
//   try {
//     const response = await axios.get<Article[]>(
//       `http://localhost:5014/Article`,
//       { data: user }
//     );
//     console.log(response.data);
//     return response.data;
//   } catch (error) {
//     console.error(error);
//     throw new Error("Failed to fetch articles");
//   }
// };

export const fetchArticles = async (user: Users): Promise<Article[]> => {
  try {
    const response = await axios.get<Article[]>(
      `http://localhost:5014/Article?email=${user.Email}`
    );
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error("Failed to fetch articles");
  }
};
