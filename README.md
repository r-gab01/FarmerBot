<p align="center">
  <img src="https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white" alt="Unity" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/Robotics-607D8B?style=for-the-badge" alt="Robotics" />
</p>

# FarmerBot: An Autonomous Agricultural Agent

**FarmerBot** is an autonomous robotic agent designed in **Unity** to manage the complete lifecycle of plants in a controlled agricultural environment. By integrating **Robotics**, **Probabilistic Models**, and **Artificial Intelligence**, the system minimizes human intervention while optimizing resources like water and soil health.

## 📺 Project Showcase

> **Watch the Demo:** (https://drive.google.com/file/d/1gdH-F6AKzDIQUXwCuQ2chOcQbq_W36FO/view?usp=drive_link)

## 🌟 Key Features

  * **Autonomous Navigation**: Path planning using **A**\* algorithm on a dynamic **NavMesh**.
  * **Intelligent Behavior**: Managed via a hierarchical **Behavior Tree (BT)** that balances reactive (emergency stops) and deliberative (harvesting strategies) actions.
  * **Knowledge-Based Reasoning**: Uses a formal **Ontology** (designed in Protégé) to manage environmental data and plant states.
  * **Probabilistic Decision Making**: Handles sensor uncertainty (e.g., GPS noise and disease detection) using **Bayesian logic**.
  * **Explainable AI (XAI)**: The robot provides real-time audio justifications for its actions, increasing transparency.
  * **Dynamic Adaptation**: Capable of re-mapping and searching for plants if they are moved by an external agent (simulated human interaction).

## 📸 Screenshots

<p align="center">
  <b>Field Overview</b><br>
  <img alt="Field Overview" src="https://github.com/user-attachments/assets/1f8ed1b4-e6e2-4369-af07-56fea68fcae8" width="100%">
</p>

<p align="center">
  <img alt="Sensor Debugging" src="https://github.com/user-attachments/assets/fcd0e6c5-4e01-4e23-b105-a608af635a60" width="49%">
&nbsp;
  <img alt="Behavior Tree Logic" src="https://github.com/user-attachments/assets/379f7fc5-ae87-46da-81bc-39b4946d8e1a" width="49%">
</p>
<p align="center">
  <i>Left: Sensor Debugging (Raycast) &nbsp; | &nbsp; Right: Behavior Tree Logic</i>
</p>



## 🛠 Tech Stack

  * **Engine**: Unity (3D Simulation).
  * **Language**: C\#.
  * **AI Frameworks**: Behavior Bricks (for BTs), Unity NavMesh (for Navigation).
  * **Knowledge Management**: Protégé & OWL/TTL (Ontology).
  * **Algorithms**: A\* Pathfinding, Bayesian Inference, Gaussian Noise Models.

## 🧠 How it Works

1.  **Perception**: The robot uses simulated **GPS, Compass, and Raycast-based optical sensors** to monitor plant moisture, growth, and health.
2.  **Reasoning**: It queries its internal **Knowledge Base** to decide if a plant needs water, is ready for harvest, or shows signs of disease.
3.  **Action**: Depending on the state, it activates **mechanical grippers** to move plants to specific storage sheds or starts the **irrigation pump**.
4.  **Self-Maintenance**: Automatically returns to the **power station** or **water tank** when internal resources are low.

## 📂 Project Structure
Key C# scripts driving FarmerBot's behavior (located in `Assets/Scripts/`):
*   **`AgentPlantManager.cs` & `PlantStats.cs`**: The knowledge base brain. Uses ontology data and probabilistic logic to evaluate plant health and harvest readiness.
*   **`KalmanFilter.cs`**: Applies Gaussian noise models to handle sensor uncertainty (e.g., GPS noise).
*   **`AStar.cs` & `NavMeshGraph.cs`**: Powers autonomous navigation and dynamic obstacle avoidance.
*   **`AgentAudioManager.cs`**: The Explainable AI (XAI) core. Translates Behavior Tree states into real-time audio justifications.
*   **`RayCast.cs` & `BumperSensor.cs`**: Simulates the robot's physical and optical hardware sensors.
*   **`HumanInteraction.cs`**: Simulates human interference, allowing runtime plant movement to test the agent's dynamic adaptation.
*   **`Actions/` & `Conditions/`**: Custom *Behavior Bricks* nodes defining the robot's decision-making logic.
*   
## 🚀 Installation & Usage
1.  **Prerequisites**: Unity Editor installed. *(Optional: Protégé to inspect Ontology files).*
2.  **Clone the Repo**: 
    ```bash
    git clone https://github.com/r-gab01/FarmerBot.git
    ```
3.  **Open Project**: Launch Unity Hub and open the `FarmerBot_Final` folder.
4.  **Load Scene**: In the Project window, navigate to `Assets/Scenes/` and open `FarmerBot.unity`.
5.  **Run Simulation**: Press the **Play `[►]`** button in the Unity Editor.
6.  **Interact**: Drag and drop plants during runtime to test the robot's real-time adaptation and path recalculation.

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
