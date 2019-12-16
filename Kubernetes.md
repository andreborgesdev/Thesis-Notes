# Kubernetes

## Background

So, Google was running search and stuff on containers, and obviously search, and even Gmail and the likes, they're pretty humongous. I mean, we're talking like billions of containers a day stuff here, which would be right, seeing as every good search runs in its own container. Well, at scale like that, you just can't have humans pushing buttons, so what they did was they built a couple of in-house systems to help. First, they built something called Borg, quality name, then they built Omega. So, Borg came first, and as you do, you learned a bunch of stuff, and they fed that into Omega. Then, for whatever reasons, they decided to build another system, obviously learning from both Borg and Omega, and they made this new one open source, and lo, Kubernetes was born. So, Kubernetes came out of Google, and it's open source. And these days, it's the superstar project for the Cloud Native Computing Foundation, and to say it's gone from strength to strength, wow, that would be an epic understatement.

From a back-end perspective, it is backed by pretty much everyone.

The cloud players are all over it, and so are the traditional IT vendors.

In fact, it's probably the most extensive, like it does stateless, stateful, batch work, long running. It does security, storage, networking, serverless, or functions as a service, machine learning. Honestly, we could be here all day. There is not a lot that Kubernetes can't do. And all of the stuff it can do, it can pretty much to anywhere. Like we said, in the cloud and on-prem in you datacenter, and even on your laptop when you're developing. 

The name Kubernetes, it's Greek for helmsman or captain, the helmsman being the person who steers the ship, which I guess is why they picked it. I mean, after all, we have got this nautical theme going on in the container ecosystem. Oh yeah, and you'll see it shortened to this quite a lot, the 8 replacing the 8 characters between the K and the S, and some people pronounce this kates.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Foundation.png?raw=true)

## Functionality

It's all about managing containerized apps at scale, and the focus is very much on the app.

Well, once we've got that, we package our apps, tell Kubernetes what they should look like, and then we just sit back, and we let Kubernetes do all the hard stuff of deploying and managing. So things like scaling, self-healing, running updates, all that stuff, no sweat. Kubernetes does it. I mean, there's obviously some up-front work from us to do like the packaging and set some of the thresholds and the likes, but honestly, with actually not a huge amount of effort from us, Kubernetes really can manage our apps, which definitely is magic, but capping it all off is the fact that it decouples our apps from any underlying environment, meaning we can switch between clouds, we can move back on-prem, and even back to the cloud again. It's all pretty easy with Kubernetes.

Docker provides the mechanics for starting and stopping individual containers, which in the grand scheme of things is pretty low-level stuff.

Well, Kubernetes, it doesn't care about lower-level stuff like that. Kubernetes cares about higher-level stuff, like how many containers to run in, maybe which nodes to run them on, and things like knowing when to scale them up or down or even how to update your containers without downtime.

If you think about your application as a musical masterpiece, I know, bear with me, if you did that, it'd be made up of lots of different musical notes from different instruments. There'd be violins, maybe they'd be front-end services, and I don't know, maybe the brass section would be the back end or whatever, but when they play together, they form this amazing musical experience. Well, if you've seen an orchestra, you know that there's a conductor at the front, and that person's in charge, and she's doing things like telling the trombones when to come in, how many violins, how loud, all of that stuff. Well, applications are similar. Loads of different parts that need to know how and where to run, which network to operate on, how many instances are required to meet demand, and probably a load more. And if this is the case, which it is, then Kubernetes is the conductor. So it's basically issuing commands to Docker instances, telling them when to start and stop containers and how to run them, sort of. And like with the orchestra, when all of this stuff comes together, they form this amazing application experience.

A bit more technical though, I guess if you know VMware at all, maybe think of Docker as ESXi, that low-level hypervisor. Then Kubernetes, I suppose, would be vCenter that sits above a bunch of hypervisors.

at the kind of high level we're at, we'd have a Kubernetes cluster down here to host our applications, and it can be anywhere. Well, each of these nodes is running some Kubernetes software and a container runtime. Usually the container runtime is Docker or containerd, but others do exist. The point is there's a container runtime on every node so that every node can run containers. Then sitting above all of this is the brains of Kubernetes, and that's making the decisions, like the conductor in the orchestra. Well, assume we've got a simple app with a web front end and a persistent back end. The web front end is maybe containerized NGINX, and let's say it's containerized MySQL on the back end. We tell Kubernetes maybe we want a single container on the back end and give it a lot of resources, like CPU and RAM, but on the front end, tell you what, we'll have two containers, but keep these smaller, and Kubernetes deploys it. So one of the thing Kubernetes does is decides which nodes to run stuff on, and it'll look something like this. And that's fine, but let's say load on the front end increases, and those two containers are not enough. Okay, no issue. Kubernetes is watching, so it sees the situation, and maybe it spins up two more, and it does it without a human getting involved. So literally, load goes up on the front end, and Kubernetes has enough intelligence not just to sit there and suffer. No, it spins up more containers. Problem averted. But the same goes if the load decreases. It's automagic. Kubernetes sees the drop in load, and it scales back down. Oh, and it's the same if a node fails or something. Seriously, Kubernetes is a fighter. It sees the node go down, and it doesn't run away and hide, and it doesn't freeze and hope the situation isn't happening. No chance. Kubernetes fights. So remember up here we asked for two web front ends, but right now we've only got one. Kubernetes observes this, and it fixes it, and we call that self-healing.

We tell Kubernetes what we want, and Kubernetes makes it happen. Then when things change, increased load, failed nodes, whatever, Kubernetes deals with it.

Docker's doing all the low-level container spinney up spinney down stuff, but it only does it when Kubernetes tells it to, meaning in this respect, Kubernetes is managing a bunch of Docker nodes.

Kubernetes is the absolute business for decoupling your applications from the underlying infrastructure. So, we've said Kubernetes runs everywhere. Kubernetes on-prem, Kubernetes in the cloud, it's all the same, meaning if your apps run on Kubernetes, it is a piece of cake migrating them on and off the cloud or even from one cloud to another. No joke. I mean, unless you're writing your apps to be tightly coupled to the services of one particular cloud, which ideally you wouldn't, but yeah, I understand why we sometimes do, but assuming you're not writing your apps to be locked to a specific cloud, then you can absolutely move seamlessly between one cloud and another and even on and off the cloud, which I think you'll agree has the potential to be huge going forward.

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Architecture.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Architecture_2.png?raw=true)

![enter image description here](https://github.com/andreborgesdev/Thesis-Notes/blob/master/Images/Kubernetes_Architecture_3.png?raw=true)

