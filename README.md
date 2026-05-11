# Real-Time Native Data Streaming and Processing Engine

This project implements a high-performance data pipeline for real-time streaming and processing between heterogeneous environments. It establishes a low-latency bridge between a Linux-based data source (Python/aiortc) and a Windows-based processing client (WPF/C#), utilizing native C++ DLLs for high-speed data transformation and synchronization.

## Technical Core Features

1. High-Performance WebRTC Pipeline
- Implemented a P2P streaming architecture using aiortc on Linux and Microsoft.MixedReality.WebRTC on Windows.
- Optimized for sub-second latency in data-intensive environments by bypassing traditional server-client bottlenecks.

2. Zero-Copy Memory Management
- Developed a memory-efficient interop layer that passes unmanaged memory pointers (IntPtr) from the C# WebRTC callback directly to the C++ Native DLL.
- Eliminated Garbage Collection (GC) overhead and CPU spikes by avoiding managed array copying of high-resolution frame buffers.

3. Pixel-Embedded Data Synchronization
- Solved the asynchronous stream synchronization issue by encoding frame metadata (IDs and Timestamps) directly into the raw pixel buffer at the source.
- Implemented a bitwise decoder in C++ to extract metadata from the received Y-plane, ensuring data determinism regardless of network jitter.

4. Native C++ Processing Engine
- Modularized computationally intensive tasks into a Native C++ DLL to leverage SIMD instructions and efficient pointer arithmetic.
- Decoupled the high-speed processing logic from the UI thread to maintain a consistent 60 FPS rendering performance.

## Tech Stack

### Remote Data Source (Linux)
- Language: Python 3.x
- Libraries: aiortc (WebRTC), NumPy, OpenCV
- Protocol: WebRTC (H.264 / I420)

### Native Processing Client (Windows)
- Framework: .NET Core (WPF), C++17 (Native)
- Architecture: MVVM (CommunityToolkit.Mvvm)
- Libraries: Microsoft.MixedReality.WebRTC

## System Architecture and Workflow

1. Data Ingestion: The Python node captures raw data and encodes synchronization barcoding into the top-row pixels of the frame buffer.
2. WebRTC Signaling: A signaling handshake establishes a peer-to-peer connection for video/data tracks.
3. Frame Interception: The WPF client intercepts incoming frames at the native layer, retrieving the raw pointer to the I420 Y-plane.
4. Native Transformation: The C++ DLL receives the pointer, decodes the embedded metadata, and performs data fusion or transformation.
5. UI Rendering: Processed results are updated via WriteableBitmap back-buffering for real-time visualization.

## Engineering Challenges and Solutions

### Memory Bottleneck Optimization
In initial prototypes, copying frame data to managed byte arrays caused significant performance degradation due to memory pressure. The system was re-architected to a Zero-Copy model where C# only acts as a thin wrapper for pointer passing, delegating all data manipulation to the C++ layer.

### Frame-Level Determinism
Traditional timestamping often loses precision over network streams. By implementing pixel-level metadata embedding, the system ensures that every frame carries its own identity. This allows the client to calculate exact latency and reconstruct the correct data sequence even in the event of packet reordering or loss.

### Thread-Safe Async Navigation
Managed the complexity of WebRTC's asynchronous signaling within the WPF UI thread using an async-based Navigation pattern. By decoupling the connection state from the view logic via DataTemplates, the system ensures a non-blocking user experience during the handshake process.