import * as signalR from "@microsoft/signalr";
import config from "../config/config";
import { loginStore } from "@/store/loginStore";

export class SignalRService {
  private connection: signalR.HubConnection;

  constructor() {
    const logStore = loginStore();
    console.log("SignalRService constructor", logStore.jwt);
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(config.signalRUrl, {
        accessTokenFactory: () => {
          return logStore.jwt as string;
        },
        withCredentials: true,
      })
      .build();
  }

  public async startConnection() {
    try {
      await this.connection.start();
      console.log("Connected to SignalR hub");
    } catch (err) {
      console.error("Error while starting connection: ", err);
    }
  }

  public async sendUpdateShoppingList(shoppingList: any) {
    try {
      await this.connection.invoke("UpdateShoppingList", shoppingList);
    } catch (error) {
      console.error("Error while sending update: ", error);
    }
  }

  public addUpdateShoppingListListener(
    updateShoppingListCallback: (updatedShoppingList: any) => void
  ) {
    this.connection.on("UpdateShoppingList", updateShoppingListCallback);
  }
}
