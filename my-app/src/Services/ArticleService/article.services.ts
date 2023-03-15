import axios from "axios";
import { Article } from "../../Models/Article";
import { Users } from "../../Models/Users";

export const fetchArticles = async (user: Users): Promise<Article[]> => {
  try {
    const response = await axios.get<Article[]>(
      `http://localhost:5014/Article?email=${user.email}`
    );
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error("Failed to fetch articles");
  }
};

export const UpdateArticleClick = async (article: Article) => {
  try {
    const response = await axios.put("http://localhost:5014/Article", article);
    console.log("Article updated:", response.data);
  } catch (error) {
    console.error("Error updating article:", error);
  }
};

export const AddOrUpdateUserClicks = async (
  article: Article,
  email: string | undefined
) => {
  try {
    const response = await axios.post(
      `http://localhost:5014/Article?email=${email}`,
      article
    );
    console.log("Article updated:", response.data);
  } catch (error) {
    console.error("Error updating article:", error);
  }
};

export const fetchPopularArticles = async (
  email: string | undefined
): Promise<Article[]> => {
  try {
    const response = await axios.get<Article[]>(
      `http://localhost:5014/Article/Popular?email=${email}`
    );
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error("Failed to fetch articles");
  }
};

export const fetchCuriousArticles = async (
  email: string | undefined
): Promise<Article[]> => {
  try {
    const response = await axios.get<Article[]>(
      `http://localhost:5014/Article/Curious?email=${email}`
    );
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
    throw new Error("Failed to fetch articles");
  }
};
